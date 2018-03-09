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
            String str = "{";
            foreach(KeyValuePair<String, object> item in data)
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

        private static String addValue(object value)
        {
            if (value is String)
            {
                return addString((String) value);
            } else
            {
                return value.ToString();
            }
        }

        private static String addString(String str)
        {
            return str;
        }
    }
}