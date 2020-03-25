using System;
using System.Collections.Generic;
using System.Text;

namespace Program.Json
{
    public class JsonListObject : JsonObject
    {
        List<JsonObject> Children;

        public bool HasChildren
        {
            get => Children != null;
        }
        public int? ChildrenCount
        {
            get => Children.Count;
        }

        public JsonListObject(List<JsonObject> children)
        {
            this.Children = children;
        }
        public JsonListObject(string id, List<JsonObject> children)
        {
            Children = children;
            Id = id;
        }

        public JsonObject GetObject(string key)
        {
            for (int i = 0; i < ChildrenCount; i++)
            {
                if (Children[i].Id != null)
                {
                    if (Children[i].Id
                            .Replace("\"", null) == key)
                        return Children[i];
                }
            }
            return null;
        }

        public void AddChild(JsonObject child)
        {
            if (child != null && HasChildren)
            {
                Children.Add(child);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public JsonObject this[string key]
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(key))
                    return GetObject(key);
                else
                    return null;
            }
        }
    }
}
