using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Program.Json
{
    public static class JsonParser
    {
        static Stack<JsonObject> objects = new Stack<JsonObject>();
        static Stack<string> Ids = new Stack<string>();
        static StringBuilder value = new StringBuilder();

        //There is no error handling.

        public static JsonObject Parse(string json)
        {
            JsonObject Root = null;

            for (int i = 0; i < json.Length; i++)
            {
                if (json[i] == ']' || json[i] == '}')
                {
                    CloseChild();
                    objects.Pop();
                }
                else if (json[i] == '[' || json[i] == '{')
                {
                    JsonObject obj = new JsonObject();
                    if (Ids.Count > 0)
                        obj.Value = Ids.Pop();

                    if (objects.Count > 0)
                        objects.Peek().AddChild(obj);
                    else
                        Root = obj;

                    objects.Push(obj);
                }
                else if(json[i] == ',')
                {
                    CloseChild();
                }
                else if(json[i] == ':')
                {
                    Ids.Push(value.ToString());
                    value.Clear();
                }
                else if ((byte)json[i] > 32)
                {
                    value.Append(json[i]);
                }
            }

            return Root;
        }

        private static void CloseChild()
        {
            if (value.Length > 0)
            {
                var child = new JsonObject();
                if (Ids.Count > 0)
                {
                    child.Value = Ids.Pop();
                    child.AddChild(new JsonObject(value.ToString()));
                }
                else
                {
                    child.Value = value.ToString();
                }

                objects.Peek().AddChild(child);
                value.Clear();
            }
        }
    }
}
