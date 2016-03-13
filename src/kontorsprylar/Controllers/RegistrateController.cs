using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using kontorsprylar.ViewModels;
using kontorsprylar.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNet.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace kontorsprylar.Controllers
{
    
    public class RegistrateController : Controller
    {
        static StoredDbContext context;
        public static DataManager dataManager;
        public RegistrateController(StoredDbContext newcontext)
        {
            context = newcontext;
            dataManager = new DataManager(context);
        }

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
                return Json(false);
            }
            UserLoginModel user = dataManager.AddCustomer(viewModel);
            //SendConfirm(user);
            LoginUser(user);
            return Json(viewModel.Email);
        }

        private void LoginUser(UserLoginModel user)
        {
            CookieOptions myCookie = new CookieOptions() { Expires = DateTime.Now.AddMinutes(30) };
            Response.Cookies.Append("Email", user.Email, myCookie);
            HttpContext.Session.SetObjectAsJson("user", user);
        }

        //void SendConfirm(UserLoginModel user)
        //{
        //    //Skapa själva epostmeddelandet med indata "from" och "to"
        //    MailMessage mail = new MailMessage("csharp@meljoner.se", user.Email);
        //    //skapa en E-postklient med server och port
        //    SmtpClient client = new SmtpClient("send.one.com", 465);
        //    //Eftersom min server kräver SSL aktiverar jag detta. Sätt till false om annan server utan SSL nyttjas
        //    client.EnableSsl = true;
        //    //Min server kräver login, lagrar mitt inlogg i en Credential.
        //    NetworkCredential myLogin = new NetworkCredential("csharp@meljoner.se", "openpassword");
        //    //Talar om hur smtpklienten ska kommunicera
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    //Stänger av default cred. för att kunna lägga till mina egna inloggningsuppgifter.
        //    client.UseDefaultCredentials = false;
        //    //Lägger på myLogin, en credential där jag lagrat användarnamn och lösenord.
        //    client.Credentials = myLogin;
        //    mail.BodyEncoding = System.Text.Encoding.UTF8;
        //    mail.Subject = "Välkomen till Kontorsprylar.se";
        //    mail.Body = "Hej " + user.FirstName + " " + user.LastName + Environment.NewLine + Environment.NewLine + "Du har nu registrerat dig hos kontorsprylar.se med kundnummer " + user.UserID + Environment.NewLine + "Tack för kaffet!";
        //    client.SendAsync(mail,"confmail");
        //}
    }
}
