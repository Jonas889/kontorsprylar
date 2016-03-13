using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using kontorsprylar.Models;
using kontorsprylar.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    public class MyPagesController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            UserLoginModel user = new UserLoginModel();
            if(HttpContext.Session.GetObjectFromJson<UserLoginModel>("user")!= null)
            {
                user = HttpContext.Session.GetObjectFromJson<UserLoginModel>("user");
                return View(user);
            }
            return RedirectToAction("Denied", "Login");
        }
    }
}
