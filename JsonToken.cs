namespace Program.Json
{
    public class JsonToken
    {
        public string id;
        public object value;
        public JsonToken(object value, string id = "")
        {
            this.id = id;
            this.value = value;
        }
    }
}