using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Templ.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagerController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccountList()
        {
            return View();
        }
    }
}