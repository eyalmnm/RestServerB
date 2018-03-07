using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RestServerB.Utils
{
    public class JsonUtils
    {
        public static string toJsonStr(Dictionary<string, object> data)
        {
            string str = "{";
            foreach(KeyValuePair<string, object> item in data)
            {
                str += addString(item.Key) + ":" + addValue(item.Value) + ",";
            }
            if (str.Length < 1)
            {
                str = str.Remove(str.Length - 1);
            }
            str += "}";
            return str;
        }

        private static string addValue(object value)
        {
            if (value is string)
            {
                return addString((string) value);
            } else
            {
                return value.ToString();
            }
        }

        private static string addString(string str)
        {
            return "\"" + str + "\"";
        }
    }
}