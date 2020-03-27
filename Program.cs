using Program.Json;
using System;

namespace Program
{
    class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            string json = @"{
              'menu': {
                'id': 'file',
                'value': 'File',
                'popup': {
                            'menuitem': [
                              {
                      'value': 'New',
                                'onclick': 'CreateDoc()'
                    },
                    {
                      'value': 'Open',
                      'onclick': 'OpenDoc()'
                    },
                    {
                      'value': 'Save',
                      'onclick': 'SaveDoc()'
                    }
                  ]
                }
              }
            }  ";
            var parsedJson = JsonParser.Parse(json);
            Console.WriteLine(parsedJson["menu"]["popup"]["menuitem"][2]);
        }
    }
}
