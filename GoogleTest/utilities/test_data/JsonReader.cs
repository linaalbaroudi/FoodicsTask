using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace CSharpNUnitProject.Utilities
{
    public class JsonReader
    {
        public JsonReader() { }

        public String ReadFile(String key) {
            String jsonString = File.ReadAllText("utilities/Test_Data/TestData.json");
            var json = JToken.Parse(jsonString);
            return json.SelectToken(key).Value<String>();
        }

        public String[] ReadArrays(String key)
        {
            String jsonString = File.ReadAllText("utilities/Test_Data/TestData.json");
            var json = JToken.Parse(jsonString);
            return json.SelectToken(key).Values<String>().ToList().ToArray();
        }
    }
}
