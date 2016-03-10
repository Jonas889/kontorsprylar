using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace kontorsprylar.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Field can't be empty")]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Field can't be empty")]
        [StringLength(20,MinimumLength = 6)]
        public string Password { get; set; }
    }
}
