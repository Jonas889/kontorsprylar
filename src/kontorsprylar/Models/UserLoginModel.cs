using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.Models
{
    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int UserID { get; set; }
        public string Accessability { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
