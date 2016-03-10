using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using kontorsprylar.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    public class SearchController : Controller
    {
        static StoredDbContext context = new StoredDbContext();
        DataManager dataManager = new DataManager(context);
        // GET: /<controller>/
        public SearchController(StoredDbContext newContext)
        {
            context = newContext;
        }
        public IActionResult Index(string query)
        {
            
            var products = from p in context.Products select p; //vet ej om detta fungerar? är väldigt osäker på context
            //just nu endast sök på produkter, inte kategorier osv
            if (!string.IsNullOrEmpty(query))
            {
                products = products.Where(p => p.ProductName.Contains(query));
            }
            return View(products);
        }
    }
}
