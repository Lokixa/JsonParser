using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Program.Json;

namespace Program
{
    class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            string sampleJson =
                     "[{\"property\":\"value\",\"anotherOne\":74103}]";
            JsonParser parser = new JsonParser(sampleJson);
            
            
            
            /*
            Go through json string;
            Check character at ith index;
            if character == jsonCharacter => jsonAction();
            else continue through string;
            */
        }
    }
}