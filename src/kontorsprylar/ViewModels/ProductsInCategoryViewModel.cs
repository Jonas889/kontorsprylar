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
        [Display(Name = "Produkter")]
        public List<ProductViewModel> Products { get; set; }

        [Display(Name = "Kategori")]
        public CategoryMenuViewModel CategoryToShow { get; set; }

        [Display(Name = "Tekniska specifikationer")]
        public List<Specification> Specifications { get; set; }
    }
}
