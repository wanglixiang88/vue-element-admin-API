using APIExample.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIExample.Controllers
{
    public class HomeController : Controller
    {
        [AuthFilterAttribute]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
