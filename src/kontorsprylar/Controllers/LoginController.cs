using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SimpleCrypto;
using kontorsprylar.Models;
using kontorsprylar.ViewModels;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNet.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    public class LoginController : Controller
    {
        static StoredDbContext context;
        public static DataManager dataManager;
        public LoginController(StoredDbContext newcontext)
        {
            context = newcontext;
            dataManager = new DataManager(context);
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return PartialView("ModalPartialLogin");
        }

        public IActionResult LogOut()
        {
            if (Request.Cookies["Email"].Count != 0)
            {
                CookieOptions myCookie = new CookieOptions() { Expires = DateTime.Now.AddDays(-1) };
                Response.Cookies.Append("Email", "", myCookie);
            }
            return RedirectToAction("Index","Home");
        }

        public IActionResult TestLogin(LoginViewModel userLogin)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Felaktiga inloggningsuppgifter");
                return null;
            }
            bool loggedIn = ValidateLogin(userLogin.Email, userLogin.Password);

            if (loggedIn)
            {
                CookieOptions myCookie = new CookieOptions() { Expires = DateTime.Now.AddMinutes(30) };
                Response.Cookies.Append("Email", userLogin.Email, myCookie);
                return Json(userLogin.Email);
            }
            ModelState.AddModelError("Error", "Felaktiga inloggningsuppgifter");
            return null;

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
            return isValidUser;
        }
    }
}
