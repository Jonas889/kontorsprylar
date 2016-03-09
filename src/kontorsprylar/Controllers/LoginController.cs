using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SimpleCrypto;
using kontorsprylar.Models;
using kontorsprylar.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    public class LoginController : Controller
    {
        static StoredDbContext context = new StoredDbContext();
        DataManager dataManager = new DataManager(context);
        public LoginController(StoredDbContext newContext)
        {
            context = newContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Registrate()
        {
            return View();
        }
        [HttpPost]
        //public IActionResult Registrate()
        //{
        //    return View();
        //}
        [HttpPost]
        public IActionResult Index(LoginViewModel userLogin)
        {
            if (!ModelState.IsValid)
                return View(userLogin);
            bool loggedIn = ValidateLogin(userLogin.Email, userLogin.Password);
            return View();
        }

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
            //Loginvalidering här
            return isValidUser;
        }
    }
}
