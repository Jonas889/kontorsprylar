using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using kontorsprylar.Models;

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
            public IActionResult AddToCart(int PID)
        {
            var model = dataManager.GetMyShoppingCart(PID);
            return PartialView("ShoppingCartPartial", model);
        }
    }
}
