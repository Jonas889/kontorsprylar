using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using kontorsprylar.ViewModels;
using kontorsprylar.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    
    public class RegistrateController : Controller
    {
        static StoredDbContext context = new StoredDbContext();
        DataManager dataManager = new DataManager(context);

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return PartialView("ModalPartialRegistrate");
        }

        [HttpPost]
        public IActionResult Index(RegistrateViewModel viewModel)

        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            dataManager.AddCustomer(viewModel);
            return RedirectToAction("index");
        }
    }
}
