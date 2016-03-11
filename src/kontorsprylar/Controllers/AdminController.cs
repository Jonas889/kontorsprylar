﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using kontorsprylar.Models;
using kontorsprylar.ViewModels;
using SimpleCrypto;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    public class AdminController : Controller
    {
        //Här ska vi ha metoder som hämtar data genom datamanager och kickar in till min AdminPageViewModel
        //När vi lägger till en produkt görs det från AddProductController. Gäller att hålla dem separerade.
        //Här kollar vi även om det var en admin som har loggat in mha. accessibility från databasen. 

        StoredDbContext context;
        public static DataManager dataManager;

        public AdminController(StoredDbContext context)
        {
            this.context = context;
        }

        public IActionResult TestAdminLogin(LoginViewModel userLogin)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Felaktiga inloggningsuppgifter");
                return null;
            }
            bool loggedIn = ValidateLogin(userLogin.Email, userLogin.Password);

            if (loggedIn)
                return RedirectToAction(nameof(AdminController.AdminPage));

            return null;

        }

        //Get and check accessibility
        private bool ValidateLogin(string eMail, string password)
        {
            bool isValidUser = false;
            PBKDF2 crypt = new PBKDF2();
            var user = dataManager.GetUser(eMail);
            if (user != null)
            {
                if (user[0] == crypt.Compute(password, user[1]))
                    isValidUser = true;
            }
            return isValidUser;
        }
        public IActionResult AdminPage()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }        



    }
}
