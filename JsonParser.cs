using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Program.Json
{
    public class JsonObject
    {
        List<JsonObject> Children { get; }
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

        public JsonObject this[int index]
        {
            get => Children[index];
        }
        public JsonObject this[string key]
        {
            get => GetObject(key);
        }

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
    }
    public class JsonParser
    {
        JsonObject Root { get; set; }
        Stack<List<JsonObject>> objectStack = new Stack<List<JsonObject>>();
        Stack<string> Ids = new Stack<string>();

        StringBuilder sb = new StringBuilder();
        readonly string json;

        public JsonParser(string Json)
        {
            json = Json;
            Parse();
        }
        public JsonObject this[string key]
        {
            get => Root[key];
        }

        void Parse()
        {
            for (int i = 0; i < json.Length; i++)
            {
                if (json[i] == '{' || json[i] == '[')
                {
                    OpenObject();
                }
                else if (json[i] == '}' || json[i] == ']')
                {
                    if (Ids.Count != objectStack.Count - 1)
                    {
                        AddChild(Ids.Pop());
                    }
                    else
                        AddChild();
                    CloseObject();
                }
                else
                {
                    if (json[i] == ':')
                    {
                        AddId();
                    }
                    else if (json[i] == ',')
                    {
                        AddChild();
                    }
                    else if ((byte)json[i] > 32)
                    {
                        sb.Append(json[i]);
                    }
                }
            }
        }
        void OpenObject()
        {
            objectStack.Push(new List<JsonObject>());
        }
        void CloseObject()
        {
            var obj = new JsonObject(children: objectStack.Pop());

            if (Ids.Count > 0)
                obj.Id = Ids.Pop();

            if (objectStack.Count > 0)
                objectStack.Peek().Add(obj);
            else
                Root = obj;
        }
        void AddChild(string id = null)
        {
            objectStack.Peek().Add(new JsonObject(id,sb.ToString()));
            sb.Clear();
        }
        void AddId()
        {
            Ids.Push(sb.ToString());
            sb.Clear();
        }

    }

}
