using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: /<controller>/
        public IActionResult AddToCart(int PID)
        {
            return View();
        }
    }
}
