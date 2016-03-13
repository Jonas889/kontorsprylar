using kontorsprylar.Models;
using kontorsprylar.ViewModels;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    public class AdminController : Controller
    {
        //Här ska vi ha metoder som hämtar data genom datamanager och kickar in till min AdminPageViewModel
        //När vi lägger till en produkt görs det från AddProductController. Gäller att hålla dem separerade.
        //Här kollar vi även om det var en admin som har loggat in mha. accessibility från databasen.

        private IApplicationEnvironment _hostingEnvironment;

        private static StoredDbContext context;
        public static DataManager dataManager;

        public AdminController(StoredDbContext newcontext, IApplicationEnvironment hostingEnvironment)
        {
            context = newcontext;

            _hostingEnvironment = hostingEnvironment;

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
                ModelState.AddModelError(string.Empty, "Felaktiga inloggningsuppgifter");
                return null;
            }
            var user = ValidateLogin(userLogin.Email, userLogin.Password);

            if (user != null && user.Accessability == "admin")
            {
                LoginUser(user);
                return RedirectToAction(nameof(AdminController.AdminPage));
            }
            ModelState.AddModelError(string.Empty, "Felaktiga inloggningsuppgifter");
            return null;
        }

        private void LoginUser(UserLoginModel user)
        {
            CookieOptions myCookie = new CookieOptions() { Expires = DateTime.Now.AddMinutes(30) };
            Response.Cookies.Append("Email", user.Email, myCookie);
            HttpContext.Session.SetObjectAsJson("user", user);
        }

        private UserLoginModel ValidateLogin(string eMail, string password)
        {
            PBKDF2 crypt = new PBKDF2();
            var user = dataManager.GetUser(eMail);
            if (user != null)
            {
                if (user.Password == crypt.Compute(password, user.PasswordSalt))
                    return user;
            }
            return null;
        }

        public IActionResult AdminPage()
        {
            if (UserHasAdminRights())
            {
                var viewModel = dataManager.GetAdminCategories();
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Denied", "Login");
            }
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            //var model = dataManager.GetProductPresentationData();
            //return View(model);
            return View();
        }

        [HttpPost]
        public IActionResult AddProducts(IList<IFormFile> files)

        {
            foreach (var file in files)

            {
                var fileName = ContentDispositionHeaderValue

                    .Parse(file.ContentDisposition)

                    .FileName

                    .Trim('"');// FileName returns "fileName.ext"(with double quotes) in beta 3

                if (fileName.EndsWith(".txt"))// Important for security if saving in webroot

                {
                    var filePath = _hostingEnvironment.ApplicationBasePath + "\\wwwroot\\" + fileName;

                    //await file.SaveAsAsync(filePath);
                }
            }
            return RedirectToAction("Admin/AdminPage");
        }





        [HttpPost]
        public IActionResult AddProduct(AddProductViewModel viewModel, IList<IFormFile> files)
        {
            if (!ModelState.IsValid) // kollar valideringen, returnerar ErrorMsges
            {
                return View(viewModel);
            }

            foreach (var file in files)

            {
                var fileName = ContentDispositionHeaderValue

                    .Parse(file.ContentDisposition)

                    .FileName

                    .Trim('"');// FileName returns "fileName.ext"(with double quotes) in beta 3

                if (fileName.EndsWith(".txt"))// Important for security if saving in webroot

                {
                    var filePath = _hostingEnvironment.ApplicationBasePath + "\\wwwroot\\" + fileName;

                    //await file.SaveAsAsync(filePath);
                }
            }

            //Lägg till rätt imglink i db här...
            dataManager.AddProduct(viewModel); //Lägger till en produkt till databasen

            return RedirectToAction(nameof(AdminController.AddProduct));
        }

        public string GetProductlistByCategory(int categoryID)
        {
            if (UserHasAdminRights())
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
            else
            {
                return null;
            }
        }

        private bool UserHasAdminRights()
        {
            var user = new UserLoginModel();
            bool isAdmin = false;
            if (HttpContext.Session.GetObjectFromJson<UserLoginModel>("user") != null)
            {
                user = HttpContext.Session.GetObjectFromJson<UserLoginModel>("user");
                if (user.Accessability == "admin")
                    isAdmin = true;
            }
            return isAdmin;
        }

      
    }
}