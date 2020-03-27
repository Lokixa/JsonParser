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
            using (StreamReader sr = new StreamReader(@"C:\Users\lubas\source\repos\JsonParser\example.json"))
            {
                json = sr.ReadToEnd();
            }
            var newton = JsonConvert.DeserializeObject<JObject>(json);
            Console.WriteLine(newton["menu"]["popup"]["menuitem"][0]);
            var lubas = JsonParser.Parse(json);
            Console.WriteLine(lubas["menu"]["popup"]["menuitem"][0]);            
        }
    }
}
