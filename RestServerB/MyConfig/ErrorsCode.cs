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

        // Image Storage 1200 - 1299
        public const int TARGET_DIRCTORY_NOT_FOUND = 1200;
        public const int TARGET_FILE_NAME_NOT_FOUND = 1201;
        public const int TARGET_FILE_ALREADY_EXIST = 1202;
        public const int TARGET_FILE_MISSING_DATA = 1203;
        public const int TARGET_FILE_FAILED_TO_RESTORED = 1204;
    }
}