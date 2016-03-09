using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SimpleCrypto;
using kontorsprylar.Models;

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
        //[HttpPost]
        //public IActionResult Registrate(T/*ask in en vymodell för registrering*/)
        //{
        //    return View();
        //}     
        //public IActionResult ValidateLogin(LoginViewModel userLogin)
        //{
        //    bool loggedIn = ValidateLogin(userLogin.email, userLogin.password)
        //    return View();
        //}

        //private bool ValidateLogin(string eMail, string password)
        //{
        //    bool isValidUser = false;
        //    PBKDF2 crypt = new PBKDF2();
        //    var user = dataManager.GetUser()
        //    //Loginvalidering här
        //    return isValidUser;
        //}
    }
}
