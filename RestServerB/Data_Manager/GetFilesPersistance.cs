using System;
using System.IO;
using System.Collections.Generic;
using RestServerB.Utils;
using RestServerB.MyConfig;

namespace RestServerB.Data_Manager
{
    public class GetFilesPersistance
    {
        private String Directory { get; set; }

        public GetFilesPersistance(String directory)
        {
            this.Directory = directory;
        }

        public String[] getFileList()
        {
            String basePath = CsConstatnts.IMAGES_BASE_PATH;
            if (true == StringUtils.IsNullOrEmpty(basePath))
            {
                basePath = CsConstatnts.IMAGES_BASE_PATH;
            }
            String path = Path.Combine(basePath, Directory);

            //Check if filePath exist
            if (false == System.IO.Directory.Exists(path))
            {
                return null; // Directory doesn't exist
            }
            String[] filesInDir = System.IO.Directory.GetFiles(path);

            if (0 < filesInDir.Length)
            {
                filesInDir = cleanPathFromFileName(filesInDir, path);
            }

            return filesInDir;
        }

        public String[] cleanPathFromFileName(String[] filesNames, String path)
        {
            if (true == StringUtils.IsNullOrEmpty(path)) {
                return null;
            }

            String[] retVal = new String[filesNames.Length];
            for (int i = 0; i < filesNames.Length; i++)
            {
                String fullFileName = filesNames[i];
                int pathIdx = fullFileName.IndexOf(path) + path.Length;
                String fileName = fullFileName.Substring(pathIdx);
                if (true == fileName.StartsWith("\\"))
                {
                    fileName = fileName.Substring(1);
                }
                retVal[i] = fileName;
            }
            return retVal;
        }
    }
}