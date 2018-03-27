using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServerB.MyConfig
{
    public static class ErrorsCode
    {
        // User 1000 - 1099
        public const int USER_NOT_FOUND = 1000;
        public const int USER_NOT_LOGGED_IN = 1001;

        // File 1100 - 1199
        public const int FILE_NOT_FOUND = 1100;
    }
}