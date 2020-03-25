using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Program.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Program
{
    class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            string json = string.Empty;
            using (StreamReader sr = new StreamReader(@".\example.json"))
            {
                json = sr.ReadToEnd();
            }
            var newton = JsonConvert.DeserializeObject<JObject>(json);
            JsonParser parser = new JsonParser(json);
            Console.WriteLine(parser["colors"][3]);
            //Console.WriteLine(newton["colors"]);
        }
    }
}
