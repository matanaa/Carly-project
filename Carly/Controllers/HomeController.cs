using Carly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carly.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private CarlyContext db = new CarlyContext();

        public ActionResult Index()
        {
            return View();
        }

        
        /*
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }*/

        public ActionResult CarChart()
        {
            return View();
        }
        public ActionResult CountryChart()
        {
            return View();
        }
        public ActionResult ColorChart()
        {
            return View();
        }
    }
}