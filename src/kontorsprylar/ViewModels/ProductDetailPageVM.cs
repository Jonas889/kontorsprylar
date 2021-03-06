﻿using kontorsprylar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.ViewModels
{
    public class ProductDetailPageVM
    {
        [Display(Name = "Produkt")]
        public ProductViewModel Product { get; set; }

        [Display(Name = "Kategori")]
        public List<CategoryMenuViewModel> CategoryToShow { get; set; }
    }
}
