using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServerB.MyConfig
{
    public static class CsConstatnts
    {
        // Application Base pathes
        public const String IMAGES_BASE_PATH = "C:\\projects\\MyDirs\\Omdan\\pictures";
        public const String ACCESS_DB_BASE_PATH = "C:\\projects\\MyDirs\\Omdan\\DB\\";

        // Server Constants
        public const String error = "error";

        public const String login = "api/Login";
        public const String userName = "name";
        public const String password = "password";
        public const String uuid = "uuid";

        public const String findFile = "api/FindFile";
        public const String fileNumber = "fileNumber";

        public const String uploadImage = "api/ImageUpload";
        public const String directory = "filePath";
        //public static final String subDirectory = "sub_directory";
        public const String image = "fileContent";
        public const String fileName = "fileName";
        public const String fileFullPath = "file_full_path";

    }
}