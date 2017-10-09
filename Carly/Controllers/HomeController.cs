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
            var allCars = (from b in db.Brands
                           join m in db.Degems on b.id equals m.BrandID
                           select new { b.BrandName, b.OriginCountry, m.DegemName, m.Color, m.Quantity });

            var carDetails = new List<CarDetails>();
            foreach (var c in allCars)
                carDetails.Add(new CarDetails
                {
                    BrandName = c.BrandName,
                    OriginCountry = c.OriginCountry,
                    DegemName = c.DegemName,
                    Color = c.Color,
                    Quantity = c.Quantity
                });

            ViewBag.carDetails = carDetails;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

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