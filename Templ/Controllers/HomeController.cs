using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Templ.Models;

namespace Templ.Controllers
{
    public class HomeController : Controller
    {
        static string CapitalizeFirstLetter(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Username, string Password)
        {
            LgEntities db = new LgEntities();
            var user = db.Accounts.Where(x => x.username == Username && x.password == Password).FirstOrDefault();
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.Role, CapitalizeFirstLetter(user.role)),
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);

                return RedirectToAction("AccountList", "Manager");
            }
            else
            {
                ViewBag.Message = "Invalid username or password";
                return View();
            }
        }
    }
}