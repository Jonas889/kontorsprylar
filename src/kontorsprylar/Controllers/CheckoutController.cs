﻿using System;
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


            var shoppingList = dataManager.GetMyShoppingCart()
                .Select(p => new ShoppingCartVM
                {
                    ProductName = p.ProductName,
                    Price = p.Price
                }).ToList();

            return View(shoppingList);

           
        }
    }
}
