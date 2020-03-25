using System;
using System.Collections.Generic;
using System.Text;
using Program.Json;

namespace Program.Json
{
    public abstract class JsonObject
    {
        string _Id;
        public string Id
        {
            get => _Id;
            set
            {
                if (_Id == null)
                {
                    _Id = value;
                }
            }
        }
        public override string ToString()
        {
            if (Id != null)
            {
                return $"Id : {Id} ";
            }

            return base.ToString();
        }
    }
}
