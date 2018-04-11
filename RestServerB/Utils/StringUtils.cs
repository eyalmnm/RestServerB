using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServerB.Utils
{
    public class StringUtils
    {
        public static bool IsNullOrEmpty(String str)
        {
            if (null == str) return true;
            if (0 == str.Trim().Length) return true;
            String stri = str.Replace("?", "").Replace("<", "").Replace(">", "").Replace("&", "")
                .Replace("\"", "").Replace("\'", "").Replace(";", "").Replace("\n", "").Replace("\r", "")
                .Replace("\t", "").Trim();
            if (0 == stri.Length) return true;
            return false;
        }

        public static String StringArray2String(String[] theArray)
        {
            return StringArray2String(theArray, ",");
        }

        public static String StringArray2String(String[] theArray, String delimeter)
        {
            String retString = "";
            for(int i = 0; i < theArray.Length; i ++)
            {
                retString += theArray[i];
                if (theArray.Length - 1 > i)
                {
                    retString += delimeter;
                }
            }
            return retString;
        }
    }
}