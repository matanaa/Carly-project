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
                           select new { b.BrandName, b.OriginCountry, m.Color, m.Quantity });

            var carDetails = new List<CarDetails>();
            foreach (var c in allCars)
                carDetails.Add(new CarDetails
                {
                    brandName = c.BrandName,
                    originCountry = c.OriginCountry,
                    color = c.Color,
                    quantity = c.Quantity
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
    }
}