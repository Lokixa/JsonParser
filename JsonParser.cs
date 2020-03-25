using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Program.Json
{
    public class JsonObject
    {
        #region Properties
        List<JsonObject> Children = new List<JsonObject>();
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
            if (child != null)
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
    public class JsonParser
    {
        #region Properties
        JsonObject Root { get; set; }
        readonly string json;
        #endregion

        Stack<JsonObject> objectStack = new Stack<JsonObject>();
        Stack<string> Ids = new Stack<string>();
        StringBuilder sb = new StringBuilder();

        public JsonParser(string Json)
        {
            json = Json;
            Parse();
        }
        public JsonObject this[string key]
        {
            get => Root[key];
        }

        #region Parsing
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
            if (Ids.Count > 0)
                objectStack.Push(new JsonObject { Id = Ids.Pop() });
            else
                objectStack.Push(new JsonObject());
        }
        void CloseObject()
        {
            var obj = objectStack.Pop();

            if (objectStack.Count > 0)
                objectStack.Peek().AddChild(obj);
            else
                Root = obj;
        }
        void AddChild()
        {
            string id = null;
            if(Ids.Count > 0)
            {
                id = Ids.Pop();
            }
            objectStack.Peek().AddChild(new JsonObject(id,sb.ToString()));
            sb.Clear();
        }
        void AddId()
        {
            Ids.Push(sb.ToString());
            sb.Clear();
        }
        #endregion
    }
}
