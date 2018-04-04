using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServerB.Utils
{
    public class ImageUploadUtils
    {
        // Works for file name of directory_file.ext file names

        public static String GetDirecroryName(String fileName)
        {
            int firstIdx = fileName.LastIndexOf('\\');
            if (0 > firstIdx)
                firstIdx = 0;
            int lastIdx = fileName.LastIndexOf('.');
            if (0 > lastIdx)
                lastIdx = fileName.Length;
            String theFileName = fileName.Substring(firstIdx, (lastIdx - firstIdx));
            int imgIdx = theFileName.IndexOf("IMG");
            if (0 > imgIdx)
                return null;
            String directoryName = theFileName.Substring(0, imgIdx);
            directoryName = directoryName.Replace('_', '\\');         
            return directoryName;
        }

        public static String getFileName(String fullFileName)
        {
            int imgIdx = fullFileName.IndexOf("IMG");
            if (0 > imgIdx)
                return null;
            String fileName = fullFileName.Substring(imgIdx);
            return fileName;
        }
    }
}