using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using kontorsprylar.Models;
using kontorsprylar.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    public class ShoppingCartController : Controller
    {
        static StoredDbContext context;
        public static DataManager dataManager;
        public ShoppingCartController(StoredDbContext newcontext)
        {
            context = newcontext;
            dataManager = new DataManager(context);
        }
        // GET: /<controller>/
        public IActionResult AddToCart(int Id, int quantity)
        {
            var kundVagn = new ShopingCart();
            if (HttpContext.Session.GetObjectFromJson<ShopingCart>("Cart") != null)
                kundVagn = HttpContext.Session.GetObjectFromJson<ShopingCart>("Cart");
            var model = dataManager.GetMyShoppingCart(kundVagn, Id, quantity);
            HttpContext.Session.SetObjectAsJson("Cart", kundVagn);
            return PartialView("ShoppingCartPartial", model);
        }
    }
}
