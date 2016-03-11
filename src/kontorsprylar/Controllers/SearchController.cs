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

            var products = from p in context.Products select p;

            if (!string.IsNullOrEmpty(query))
            {
                products = products.Where(p => p.ProductName.Contains(query));
            }
            return View(products);
        }

       
        public IActionResult SearchResult(string query)
        {
            var products = context.Products
                .Where(p => (p.ProductName.Contains(query)) || p.Price == (float)Convert.ToDouble(query))
                .Select(p => new ViewModels.ProductViewModel
                {
                    ProductName = p.ProductName,
                    ID = p.ProductID,
                    Price = p.Price

                }).ToList();

            return View(products);


            //if (!string.IsNullOrEmpty(query))
            //{
            //    products = products.Where(p => p.ProductName.Contains(query));
            //}




        }

        //sök efter pris

        //public IActionResult SearchResult(string query)
        //{
        //    double q = Convert.ToDouble(query);
        //    var products = context.Products
        //        .Where(p => p.Price == (float)q)
        //        .Select(p => new ViewModels.ProductViewModel
        //        {
        //            ProductName = p.ProductName,
        //            ID = p.ProductID,
        //            //Price = p.Price

        //        }).ToList();

        //    return View(products);


        //    //if (!string.IsNullOrEmpty(query))
        //    //{
        //    //    products = products.Where(p => p.ProductName.Contains(query));
        //    //}




        //}
    }
}
