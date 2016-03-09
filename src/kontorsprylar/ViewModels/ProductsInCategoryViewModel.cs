using kontorsprylar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.ViewModels
{
    public class ProductsInCategoryViewModel
    {
        [Display(Name = "ProductID")]
        public int ID { get; set; }

        [Display(Name = "Produktnamn")]
        public string ProductName { get; set; }

        [Display(Name = "Produktbeskrivning")]
        public string Description { get; set; }

        [Display(Name = "Pris")]
        public float Price { get; set; }

        [Display(Name = "Kampanjpris")]
        public float CampaignPrice { get; set; }

        [Display(Name = "Lagersaldo")]
        public int StockQuantity { get; set; }

        [Display(Name = "Produktbild")]
        public string PictureSrc { get; set; }

        [Display(Name = "Tillgänglig")]
        public bool ForSale { get; set; }

        [Display(Name = "Rabatt")]
        public float DiscountPercentage { get; set; }

        [Display(Name = "Kategorier")]
        public List<CategoryMenuViewModel> Categories { get; set; }

        [Display(Name = "Taggar")]
        public List<Tagg> Taggs { get; set; }

        [Display(Name = "Tekniska specifikationer")]
        public List<Specification> Specifications { get; set; }
    }
}
