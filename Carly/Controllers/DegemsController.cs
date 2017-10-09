using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Carly.Models;
using Newtonsoft.Json;
using BayesSharp;

namespace Carly.Controllers
{
    public class DegemsController : Controller
    {
        private CarlyContext db = new CarlyContext();

        // GET: Degems
        public ActionResult Index()
        {
            var degems = db.Degems.Include(d => d.Brand);
            
                return View(degems.ToList());

        }

        // GET: Degems/Search
        public ActionResult Search(string BrandSearchString, string ColorSearchString, string ModelSearchString, string CountrySearchString)
        {
            
               var degemList = new List<Brand>();
            var carList = new List<CarDetails>();
            // find all degems in the database
            // for the droplinkbutton

            var DegemQry = from s in db.Brands
                           orderby s.BrandName
                           select s;

            //join table of all cars-details
                var allCars = (from b in db.Brands
                           join m in db.Degems on b.id equals m.BrandID
                               where ((String.IsNullOrEmpty(ColorSearchString)) || m.Color.Contains(ColorSearchString)) && ((String.IsNullOrEmpty(BrandSearchString) || b.id.ToString().Equals(BrandSearchString)) && ((String.IsNullOrEmpty(ModelSearchString) || m.DegemName.Contains(ModelSearchString)) && ((String.IsNullOrEmpty(CountrySearchString) || b.OriginCountry.Contains(CountrySearchString)))))
                               select new { b.BrandName, b.OriginCountry, m.DegemName, m.Color, m.Quantity }).Distinct();
            
            foreach (var c in allCars)
                carList.Add(new CarDetails
                {
                    BrandName = c.BrandName,
                    OriginCountry = c.OriginCountry,
                    DegemName = c.DegemName,
                    Color = c.Color,
                    Quantity = c.Quantity
                });

            degemList.AddRange(DegemQry.Distinct());
            ViewBag.degemList= degemList;
            ViewBag.carList = carList;



            return View(carList);
        }

        // GET: Degems/Search
        public ActionResult algo()
        {
            var bayesCLS = new BayesSimpleTextClassifier(); //Naive Bayes object https://github.com/afonsof/BayesSharp
            var degems = db.Degems.Include(d => d.Brand);// take list of all cars
            List<string> goodCar = new List<string>();//list of words for good car TODO:take from DB
            List<string> badCar = new List<string>();//list of words for bad car TODO:take from DB
            //testing add some words
            goodCar.Add("good car");
            goodCar.Add("great!");
            goodCar.Add("what a wonderfull piece");
            badCar.Add("bad car");

            foreach (var good in goodCar)//lets train the good part
            {
                bayesCLS.Train("good", good);
            }
            foreach (var bad in badCar)//lets train the bad part
            {
                bayesCLS.Train("bad", bad);
            }
            var maxScore = -1.0;//save the computed score
            var  favoriteCar=new Degem();//save the bast car
            foreach (var car in degems)// move on each car and check the score
            {
                //save the score
                var good = 0.0;
                var bad = 0.0;
                foreach (var p in car.Comments)// move on each post and check the score
                {
                    var result = bayesCLS.Classify(p.ContentInfo);//:)
                    if (result.ContainsKey("good")) { //check if have any resule
                        good += result["good"]/car.Comments.Count();//if yes normelaize it and save it
                    }
                    if (result.ContainsKey("bad"))
                    {
                        bad += result["bad"] / car.Comments.Count();
                    }
                }
                if (good - bad > maxScore)//check the current car score
                {
                    maxScore = good - bad;
                     favoriteCar = car;//if is max save it
                }
            }
            return View("Details", favoriteCar);//return the bast car
        }


        // GET: Degems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Degem degem = db.Degems.Find(id);
            if (degem == null)
            {
                return HttpNotFound();
            }
            return View(degem);
        }

        // GET: Degems/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }
            ViewBag.BrandID = new SelectList(db.Brands, "id", "BrandName");
            return View();
        }

        // POST: Degems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DegemId,DegemName,Color,BrandID,Quantity,VideoLink,ImageLink")] Degem degem)
        {
            if (ModelState.IsValid)
            {
                db.Degems.Add(degem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandID = new SelectList(db.Brands, "id", "BrandName", degem.BrandID);
            return View(degem);
        }

        // GET: Degems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Degem degem = db.Degems.Find(id);
            if (degem == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.Brands, "id", "BrandName", degem.BrandID);
            return View(degem);
        }

        // POST: Degems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DegemId,DegemName,Color,BrandID,Quantity,VideoLink,ImagesLinks")] Degem degem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(degem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandID = new SelectList(db.Brands, "id", "BrandName", degem.BrandID);
            return View(degem);
        }

        // GET: Degems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Degem degem = db.Degems.Find(id);
            if (degem == null)
            {
                return HttpNotFound();
            }
            return View(degem);
        }

        // POST: Degems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Degem degem = db.Degems.Find(id);
            db.Degems.Remove(degem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult BrandsStatistics()
        {
            var brands = db.Degems.GroupBy(x=>new { x.Brand.BrandName }).Select(g=>new { label =g.Key.BrandName, count = g.Sum(am => am.Quantity) });
            if (brands.Count() > 0)
            {
                string json = JsonConvert.SerializeObject(brands.ToArray());
                return Json(brands.ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json("{}");
        }

        public JsonResult CountryStatistics()
        {
            var brands = db.Degems.GroupBy(x => new { x.Brand.OriginCountry }).Select(g => new { label = g.Key.OriginCountry, count = g.Sum(am=>am.Quantity) });
            if (brands.Count() > 0)
            {
                string json = JsonConvert.SerializeObject(brands.ToArray());
                return Json(brands.ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json("{}");
        }
        public JsonResult ColorStatistics()
        {
            var brands = db.Degems.GroupBy(x => new { x.Color }).Select(g => new { label = g.Key.Color, count = g.Sum(am => am.Quantity) });
            if (brands.Count() > 0)
            {
                string json = JsonConvert.SerializeObject(brands.ToArray());
                return Json(brands.ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json("{}");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
