using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Program.Json
{
    public class JsonParser
    {
        JsonObject Root { get; set; }
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

        #region MainFunction
        Stack<JsonObject> objectStack = new Stack<JsonObject>();
        Stack<string> Ids = new Stack<string>();
        StringBuilder sb = new StringBuilder();

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

            if(Root == null)
            {
                Root = obj;
            }
            else
            {
                objectStack.Peek().AddChild(obj);
            }
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
