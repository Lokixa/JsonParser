using System;
using System.Collections.Generic;
using System.Text;

namespace Program.Json
{
    class JsonValueObject : JsonObject
    {
        string _Value;
        public string Value
        {
            get => _Value;
            set
            {
                if (_Value == null && !string.IsNullOrEmpty(value))
                {
                    _Value = value;
                }
            }
        }

        public JsonValueObject(string value)
        {
            this.Value = value;
        }
        public JsonValueObject(string id, string value)
        {
            this.Id = id;
            this.Value = value;
        }
    }
}
