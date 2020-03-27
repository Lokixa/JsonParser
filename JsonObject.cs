using System;
using System.Collections.Generic;
using System.Text;
using Program.Json;

namespace Program.Json
{
    public class JsonObject
    {
        public string Value { get; set; }
        public List<JsonObject> Children { get; set; }

        public JsonObject()
        {
            //Used for a minimal replacement of the builder pattern
        }
        public JsonObject(string value)
        {
            Value = value;
        }
        
        public void AddChild(JsonObject child)
        {
            if (Children == null) Children = new List<JsonObject>();
            Children.Add(child);
        }

        JsonObject FindObject(string key)
        {
            if(Children != null)
            {
                foreach(var child in Children)
                {
                    if (child.Value == key)
                        return child;
                }
            }
            return null;
        }

        public JsonObject this[int index]
        {
            get
            {
                return Children?[index];
            }
        }
        public JsonObject this[string key]
        {
            get
            {
                string _key = $"\"{key}\"";
                return FindObject(_key);
            }
        }
        public override string ToString()
        {
            string toReturn = string.Empty;
            if (Children != null)
            {
                toReturn = $"[{string.Join(",", Children)}]";
                if (Value != null)
                    toReturn = $"{Value}:" + toReturn;
            }
            else if (Value != null)
                toReturn = Value;
            return toReturn;
        }

    }
}
