using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using kontorsprylar.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    public class StoreController : Controller
    {
            static StoredDbContext context;
            public StoreController(StoredDbContext newcontext)
            {
                context = newcontext;
                dataManager = new DataManager(context);
            }
            //Denna körs innan context ovan är instansierad.. Då skickar vi in context = null till konstruktorn för DataManager
            public static DataManager dataManager; // = new DataManager(context);
                                                   // GET: /<controller>/
            public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Category(int ID)
        {
            var model = dataManager.GetProductsInCategory(ID);
            return View(model);
        }
        //[HttpGet]
        //public IActionResult Product(int ID)
        //{
        //    var model = dataManager.GetProduct(ID);
        //    return View(model);
        //}
    }
}
