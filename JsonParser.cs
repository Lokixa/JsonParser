using System;
using System.Collections.Generic;

namespace Program.Json
{
    public class JsonParser
    {
        private readonly string Json;
        private int Index = 0;
        List<JsonToken> Tokens = new List<JsonToken>();
        public JsonParser(string Json)
        {
            this.Json = Json;
            ParseJson();
        }
        private void ParseJson()
        {
            for (; Index < Json.Length; Index++)
            {
                switch (Json[Index])
                {
                    case '"':
                        GetValue();
                        break;
                }
            }
        }

        private void GetValue()
        {
            int lastIndex = Index;
            Index = Json.IndexOf('"',Index+1);
            string id = Json.Substring(lastIndex,Index - lastIndex);
            Console.WriteLine($"Id: {id}");
            
        }
    }
}