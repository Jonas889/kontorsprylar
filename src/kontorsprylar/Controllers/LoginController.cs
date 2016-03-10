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

 
        
        public IActionResult TestLogin(LoginViewModel userLogin)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Felaktiga inloggningsuppgifter");
                return null;
            }
            bool loggedIn = ValidateLogin(userLogin.Email, userLogin.Password);

            if (loggedIn)
                return Json(userLogin.Email);

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
