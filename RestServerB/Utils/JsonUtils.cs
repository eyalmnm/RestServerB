using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RestServerB.Utils
{
    public class JsonUtils
    {
        public static String toJsonStr(Dictionary<String, object> data)
        {
            String jsonString = "{";
            foreach (KeyValuePair<String, object> item in data)
            {
                jsonString += '\"' + item.Key + '\"';
                jsonString += ':';
                if (item.Value is string)
                {
                    jsonString += '\"' + ((string)item.Value) + '\"';
                }
                else
                {
                    jsonString += ((string)item.Value.ToString());
                }
                jsonString += ',';
            }
            if (1 < jsonString.Length)
            {
                jsonString = jsonString.Remove(jsonString.Length - 1);
            }
            jsonString += "}";
            Console.WriteLine("Json: " + jsonString);
            return jsonString;
        }
    }
}