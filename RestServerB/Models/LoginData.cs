using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServerB.Models
{
    public class LoginData
    {
        private String Name { set; get; }
        private String Password { set; get; }

        public LoginData(String name, String password)
        {
            Console.WriteLine($"enter with {name}");
            this.Name = name;
            this.Password = password;
        }

        public String getName()
        {
            return this.Name;
        }

        public String getPassword()
        {
            return this.Password;
        }
    }
}