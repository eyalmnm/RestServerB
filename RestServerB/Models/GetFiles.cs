using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServerB.Models
{
    public class GetFiles
    {
        private String Dirctory { get; set; }

        public GetFiles(String directory)
        {
            this.Dirctory = directory;
        }

        public String getDirectory()
        {
            return Dirctory;
        }
    }
}