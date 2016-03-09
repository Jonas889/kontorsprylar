using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.ViewModels
{
    public class AddProductViewModel
    {
        [Display(Name = "Produktnamn")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fyll i produktnamn")]
        public string ProductName { get; set; }

        [Display(Name = "Produktbeskrivning")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fyll i beskrivning")]
        public string Description { get; set; }

        [Display(Name = "Pris")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fyll i pris")]
        public float Price { get; set; }

        [Display(Name = "Kampanjpris")]
        public float CampaignPrice { get; set; }

        [Display(Name = "Antal")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fyll i antal")]
        public int StockQuantity { get; set; }

        [Display(Name = "Bild")]        
        public string Picture { get; set; }

        [Display(Name = "Tillgänglig")]
        public bool ForSale { get; set; }
    }
}
