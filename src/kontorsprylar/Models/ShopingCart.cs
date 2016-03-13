using kontorsprylar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kontorsprylar.Models
{
    public class ShopingCart
    {
        public List<ShoppingCartVM> KundVagn;
        public ShopingCart()
        {
            KundVagn = new List<ShoppingCartVM>();
        }
        public List<CategoryMenuViewModel> CategoryMenu { get; set; }

    }
}
