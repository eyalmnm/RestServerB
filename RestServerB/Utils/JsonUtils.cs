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
                    String value = (string)item.Value;
                    if (value.Contains("\""))
                    {
                        value = value.Replace('\"', '\'');
                    }
                    jsonString += '\"' + value + '\"';
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

        public static String toJsonStr(List<Dictionary<String, object>> dataList)
        {
            if (null == dataList) return null;
            String jsonStr = "[";
            foreach (Dictionary<String, object> item in dataList)
            {
                jsonStr += toJsonStr(item);
                jsonStr += ",";
            }
            if (1 < jsonStr.Length)
            {
                jsonStr = jsonStr.Remove(jsonStr.Length - 1);
            }
            jsonStr += "]";
            return jsonStr;
        }
    }
}