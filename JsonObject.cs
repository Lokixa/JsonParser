using System;
using System.Collections.Generic;

namespace Program.Json
{
    public class JsonObject
    {
        public string Value { get; set; }
        public List<JsonObject> Children { get; private set; }

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
                    //Removes parentheses
                    string normalizedKey = child.Value.Substring(1, child.Value.Length - 2);

                    if (normalizedKey == key)
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
                return FindObject(key);
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
