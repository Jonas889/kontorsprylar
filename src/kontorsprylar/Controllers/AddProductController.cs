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
    public class AddProductController : Controller
    {
        static StoredDbContext context = new StoredDbContext();
        DataManager dataManager = new DataManager(context);
        public AddProductController(StoredDbContext newContext)
        {
            context = newContext;
        }

        //Denna häntar in produktdata som ska visas vid edit.. Lös på bättre sätt
        public IActionResult Index()
        {
            var model = dataManager.GetProductPresentationData();
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddProductViewModel viewModel)
        {
            if (!ModelState.IsValid) // kollar valideringen, returnerar ErrorMsges
            {
                return View(viewModel);
            }

            dataManager.AddProduct(viewModel);

            return RedirectToAction(nameof(AddProductController.Add));
        }


    }
}