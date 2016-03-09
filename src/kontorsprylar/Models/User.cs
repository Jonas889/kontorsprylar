using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int Accessability  { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string Phone { get; set; }
        public  string Password { get; set; }
        public string CompanyName { get; set; }
    }
}
