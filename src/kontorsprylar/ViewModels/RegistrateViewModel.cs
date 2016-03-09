using Övning3.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.ViewModels
{
    public class RegistrateViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Obligatoriskt"), Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Obligatoriskt"), Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Obligatoriskt"), Display(Name = "Gatuadress")]
        public string Street { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Obligatoriskt"), Display(Name = "Postkod")]
        public string Zip { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Obligatoriskt"), Display(Name = "Postort")]
        public string City { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Obligatoriskt"), Display(Name = "Land")]
        //public string Country { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Obligatoriskt"), Display(Name = "Mobilnummer"), StringLength(10,MinimumLength =10, ErrorMessage = "Ange ett mobilnummer i formatet 07xxxxxxxx")]
        public string CellPhone { get; set; }

        [Display(Name = "Fast telefon (valfritt)")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Obligatorisk"), EmailAddress(ErrorMessage = "Ange en korrekt e-postaddress"), Display(Name = "E-post")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Obligatoriskt"), Display(Name = "Lösenord")] //<-- fixas
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Företag (valfritt)")]
        public string CompanyName { get; set; }

        [MustBeTrueAttribute(ErrorMessage = "Var god acceptera de allmänna villkoren")]
        public bool AcceptTerms { get; set; }
    }
}