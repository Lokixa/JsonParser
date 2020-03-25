using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Program.Json
{
	public class JsonParser
	{
		JsonListObject Root { get; set; }
		readonly string json;
		
		public JsonParser(string Json)
		{
			json = Json;
			Parse();
		}
		public JsonObject this[string key]
		{
			get => Root[key];
		}

		#region MainFunction
		Stack<JsonObject> objectStack = new Stack<JsonObject>();
		Stack<string> Ids = new Stack<string>();
		StringBuilder value = new StringBuilder();
		
		void Parse()
		{
			for (int i = 0; i < json.Length; i++)
			{
				if (json[i] == '{' || json[i] == '[')
				{
					CreateObject();
				}
				else if (json[i] == '}' || json[i] == ']')
				{
					CreateObject(); //For last child object

					CloseObject();
				}
				else
				{
					if (json[i] == ':')
					{
						AddId();
					}
					else if (json[i] == ',')
					{
						CreateObject();
					}
					else if ((byte)json[i] > 32)
					{
						value.Append(json[i]);
					}
				}
			}
		}
		void CloseObject()
		{
			var obj = objectStack.Pop();

			if(objectStack.Count > 0)
			{
				((JsonListObject)objectStack.Peek()).AddChild(obj);
			}
		}
		void CreateObject()
		{
			JsonObject obj = null;
			//Can refactor to builder pattern.
			if (value.Length > 0)
			{
				obj = new JsonValueObject(value.ToString());
				value.Clear();
			}
			if (Ids.Count > 0)
			{
				obj.Id = Ids.Pop();
			}
			

			objectStack.Push(obj);
				
		}
		void AddId()
		{
			Ids.Push(value.ToString());
			value.Clear();
		}
		#endregion
	}
}
