using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServerB.Models
{
    public class LoginData
    {
        private String Name { get; }
        private String Password { get; }

        public LoginData(String name, String password)
        {
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