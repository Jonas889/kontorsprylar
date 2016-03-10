using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using kontorsprylar.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    public class AdminPageController : Controller
    {
        //Här ska vi ha metoder som hämtar data genom datamanager och kickar in till min AdminPageViewModel
        //När vi lägger till en produkt görs det från AddProductController. Gäller att hålla dem separerade.

        StoredDbContext context;

        public AdminPageController(StoredDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            //Måste lägga in validering här, så att admin faktiskt har lyckats logga in. 
            var dataManager = new DataManager(context);
            var model = dataManager.GetListboxItems(); //Skapa metoden....
            return View(model);
        }
    }
}
