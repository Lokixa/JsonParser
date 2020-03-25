using System;
using System.Collections.Generic;
using System.Text;

namespace Program.Json
{
    public class JsonObject
    {
        #region Properties
        List<JsonObject> Children;
        string _Id;
        public string Id
        {
            get => _Id;
            set
            {
                if(_Id == null)
                {
                    _Id = value;
                }
            }
        }

        string _Value;
        public string Value {
            get => _Value; 
            set {
                if (_Value == null && !string.IsNullOrEmpty(value))
                {
                    _Value = value;
                }
            }
        }

        public bool HasChildren
        {
            get => Children != null;
        }
        public int? ChildrenCount
        {
            get => Children.Count;
        }
        #endregion

        #region Constructors
        public JsonObject()
        {

        }
        public JsonObject(string value)
        {
            Value = value;
        }
        public JsonObject(List<JsonObject> children)
        {
            this.Children = children;
        }
        public JsonObject(string id, string value)
        {
            Id = id;
            Value = value;
        }
        public JsonObject(string id, List<JsonObject> children)
        {
            Children = children;
            Id = id;
        }
        #endregion

        #region Indexers
        public JsonObject this[int index]
        {
            get => Children[index];
        }
        public JsonObject this[string key]
        {
            get => GetObject(key);
        }
        #endregion

        #region Methods
        public JsonObject GetObject(string key)
        {
            for(int i = 0; i < ChildrenCount; i++)
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
                throw new ArgumentNullException();
        }
        #endregion

        #region Useless
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if(Id != null)
            {
                sb.Append($"Id : {Id} ");
            }
            if(Value != null)
            {
                sb.Append($"Value : {Value} ");
            }
            if (HasChildren)
            {
                sb.Append($"Children : {ChildrenCount} ");
            }

            return sb.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return (Id == null) && (!HasChildren) && (Value == null);
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
