using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiWeb.Models
{
    public class Users
    {
        public string user_name { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public Users(string user_name, string password, string email)
        {
            this.user_name = user_name;
            this.password = password;
            this.email = email;
        }
    }
}