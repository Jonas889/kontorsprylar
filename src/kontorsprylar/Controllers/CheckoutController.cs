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
    public class CheckoutController : Controller
    {
        static StoredDbContext context;
        public CheckoutController(StoredDbContext newcontext)
        {
            context = newcontext;
            dataManager = new DataManager(context);
        }
        //Denna körs innan context ovan är instansierad.. Då skickar vi in context = null till konstruktorn för DataManager
        public static DataManager dataManager;
        // GET: /<controller>/
        public IActionResult Index()
        {
            var shoppingList = new ShopingCart(); ;
            if (HttpContext.Session.GetObjectFromJson<ShopingCart>("Cart") != null)
                shoppingList = HttpContext.Session.GetObjectFromJson<ShopingCart>("Cart");
                //.Select(p => new ShoppingCartVM
                //{
                //    ProductName = p.ProductName,
                //    Price = p.Price,
                //    ProductQuantity = p.ProductQuantity,
                //    ProductID = p.ProductID

                //}).ToList();

            return View(shoppingList);

           
        }

        public IActionResult Delete(int productID)
        {
            var shoppingList = new ShopingCart(); ;
            if (HttpContext.Session.GetObjectFromJson<ShopingCart>("Cart") != null)
                shoppingList = HttpContext.Session.GetObjectFromJson<ShopingCart>("Cart");
            var result = dataManager.DeleteFromCart(shoppingList, productID);
            HttpContext.Session.SetObjectAsJson("Cart", result);
            return RedirectToAction("index", result);
        }

        public IActionResult Update(int productID)
        {
            throw new NotImplementedException();
        }
        
    }
}
