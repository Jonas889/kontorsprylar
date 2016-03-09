using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.ViewModels
{
    public class AddProductViewModel
    {
        [Display(Name = "Produkt namn")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fyll i produktnamn")]
        public string ProductName { get; set; }

        [Display(Name = "Produkt beskrivning")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fyll i beskrivning")]
        public string Description { get; set; }

        [Display(Name = "Pris")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fyll i pris")]
        public float Price { get; set; }

        [Display(Name = "Kampanj pris")]
        public float CampaignPrice { get; set; }

        [Display(Name = "Antal")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fyll i antal")]
        public int StockQuantity { get; set; }

        [Display(Name = "Bild")]        
        public string ImgLink { get; set; }

        [Display(Name = "Rea")]
        public bool ForSale { get; set; }
    }
}
