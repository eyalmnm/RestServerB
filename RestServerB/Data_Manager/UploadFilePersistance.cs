using System;
using System.IO;
using System.Web;
using RestServerB.Utils;
using RestServerB.MyConfig;
using System.Drawing;

// Ref: https://stackoverflow.com/questions/16751372/i-would-like-to-create-a-folder-to-store-images-in-c-sharp-web-application

namespace RestServerB.Data_Manager
{
    public class UploadFilePersistance
    {
        public static String IMAGES_BASE_DIRECTORY = "C:\\projects\\MyDirs\\Omdan\\pictures\\";

        private String Directory { get; set; }
        private String FileName { get; set; }
        private String FileContent { get; set; }

        public UploadFilePersistance(String directory, String fileName, String fileContent)
        {
            this.Directory = directory;
            this.FileName = fileName;
            this.FileContent = fileContent;
        }

        /*
         * Throws Excption with error code
         */ 
        public String SaveInDirectory()
        {
            if ((true == StringUtils.IsNullOrEmpty(this.Directory)) 
                || (true == StringUtils.IsNullOrEmpty(this.FileName)) 
                || (true ==StringUtils.IsNullOrEmpty(this.FileContent))) {
                throw new Exception(ErrorsCode.TARGET_FILE_MISSING_DATA.ToString());
            }
            // Image path           
            String basePath = CsConstatnts.IMAGES_BASE_PATH; // HttpContext.Current.Server.MapPath("/../../Pictures");
            if (true == StringUtils.IsNullOrEmpty(basePath))
            {
                basePath = CsConstatnts.IMAGES_BASE_PATH;
            }
            String path = Path.Combine(basePath, Directory);           

            //Check if filePath exist
            if (false == System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create filePath if it doesn't exist
            }
          
            String imagePath = Path.Combine(path, this.FileName);
            // Check whether this file already exist
            if (true == System.IO.File.Exists(imagePath))
            {
                throw new Exception(ErrorsCode.TARGET_FILE_ALREADY_EXIST.ToString());
            }

            // Convert Base64 String to Image
            Image image = ImageUtils.Base64ToImage(this.FileContent);
            if (null == image)
                return null;

            // Saving the image
            image.Save(imagePath);

            return Path.Combine(this.Directory, this.FileName);
        }
    }
}