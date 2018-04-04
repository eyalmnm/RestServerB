using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

// Ref: https://stackoverflow.com/questions/330346/c-sharp-read-a-jpeg-from-file-and-store-as-an-image
// Ref: https://stackoverflow.com/questions/12909905/saving-image-to-file

namespace RestServerB.Utils
{
    public class FileUtils
    {
        public static bool SaveToFile(Image image, String path, String fileName)
        {
            if ((null != image) && (false == StringUtils.IsNullOrEmpty(path)) 
                && (false == StringUtils.IsNullOrEmpty(fileName)))
            {
                image.Save(path + "\\" + fileName, ImageFormat.Png);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Image getImage(String path, String fileName)
        {
            if ((false == StringUtils.IsNullOrEmpty(path) 
                && (false == StringUtils.IsNullOrEmpty(fileName)))) {
                return Image.FromFile(path + "\\" + fileName);
            } else
            {
                return null;
            }      
        }
    }
}