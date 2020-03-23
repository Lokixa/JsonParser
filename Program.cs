using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Program.Json;

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
            JsonParser parser = new JsonParser(json);
            var a = parser["colors"];
            Console.WriteLine(a[2]);
        }
    }
}
