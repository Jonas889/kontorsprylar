using kontorsprylar.Models;
using kontorsprylar.ViewModels;
using Microsoft.AspNet.Mvc;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    public class AdminController : Controller
    {
        //Här ska vi ha metoder som hämtar data genom datamanager och kickar in till min AdminPageViewModel
        //När vi lägger till en produkt görs det från AddProductController. Gäller att hålla dem separerade.
        //Här kollar vi även om det var en admin som har loggat in mha. accessibility från databasen.

        private static StoredDbContext context;
        public static DataManager dataManager;

        public AdminController(StoredDbContext newcontext)
        {
            context = newcontext;
            dataManager = new DataManager(context);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel userLogin)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Felaktiga inloggningsuppgifter");
                return View(userLogin);
            }
            bool loggedIn = ValidateLogin(userLogin.Email, userLogin.Password);

            if (loggedIn)
                return RedirectToAction(nameof(AdminController.AdminPage));

            return View(userLogin);
        }

        //Get and check accessibility
        private bool ValidateLogin(string eMail, string password)
        {
            bool isValidUser = false;
            PBKDF2 crypt = new PBKDF2();
            var user = dataManager.GetAdmin(eMail); //Hämtar Password, PasswordSalt och Accessability
            if (user != null)
            {
                if (user[0] == crypt.Compute(password, user[1]) && user[2] == "admin") //Sätter ihop password och passwordsalt samt kollar att det var en admin
                    isValidUser = true;
            }
            return isValidUser;
        }

        public IActionResult AdminPage()
        {
            var viewModel = dataManager.GetAdminCategories();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            //var model = dataManager.GetProductPresentationData();
            //return View(model);
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductViewModel viewModel)
        {
            if (!ModelState.IsValid) // kollar valideringen, returnerar ErrorMsges
            {
                return View(viewModel);
            }

            dataManager.AddProduct(viewModel); //Lägger till en produkt till databasen

            return RedirectToAction(nameof(AdminController.AddProduct));
        }

        public string GetProductlistByCategory(int categoryID)
        {
            var result = context.Products
                .Where(p => p.CategoryID == categoryID)
                .OrderBy(p => p.ProductID)
                .Select(p => new ProductViewModel
                {
                    ID = p.ProductID,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    CampaignPrice = p.CampaignPrice,
                    ImgLink = p.ImgLink,
                    Description = p.Description,
                    StockQuantity = p.StockQuantity,
                    ForSale = p.ForSale,
                    CategoryID = p.CategoryID,
                    Specifications = null
                }).ToList();

            return JsonConvert.SerializeObject(result);
        }
    }
}