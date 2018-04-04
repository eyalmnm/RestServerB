using System;
using System.Text;

// Ref: https://stackoverflow.com/questions/27972078/encoding-hebrew-in-java-and-json

namespace RestServerB.Models
{
    public class UploadedFile
    {
        private String FilePath { get; set; }
        private String FileName { get; set; }
        private String FileContent { get; set; }

        public UploadedFile(String filePath, String fileName, String fileContent)
        {         
            this.FilePath = filePath;
            this.FileName = fileName;
            this.FileContent = fileContent;
        }

        public String getFilePath()
        {
            return this.FilePath;
        }

        public String getFileName()
        {
            return this.FileName;
        }

        public String getFileContent()
        {
            return this.FileContent;
        }
    }
}