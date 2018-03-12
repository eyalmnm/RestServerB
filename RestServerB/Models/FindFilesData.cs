using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServerB.Models
{
    public class FindFilesData
    {
        private String FileNumber { get; set; }

        public FindFilesData(String fileNumber)
        {
            Console.WriteLine($"Searching: {fileNumber}");
            this.FileNumber = fileNumber;
        }

        public String getFileNumber()
        {
            return this.FileNumber;
        }
    }
}