using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Carly.Models;

namespace Carly.Controllers
{
    public class DegemsController : Controller
    {
        private CarlyContext db = new CarlyContext();

        // GET: Degems
        public ActionResult Index()
        {
            var degems = db.Degems.Include(d => d.Brand);

            //if (User.IsInRole("Admin"))
                return View(degems.ToList());
         //   return View("ReadOnlyIndex", degems.ToList());

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
            //.CarDetails.AddRange(carList.Distinct());
            //db.SaveChanges();
            degemList.AddRange(DegemQry.Distinct());
            ViewBag.degemList= degemList;
            ViewBag.carList = carList;

            //get the data to table
            
            /*var cars = from b in db.CarDetails select b;
            if (ColorSearchString == null)
            {
                carList = carList.Where(b => b.Color.Contains(ColorSearchString));
            }

            if (!String.IsNullOrEmpty(BrandSearchString))
            {
                cars = cars.Where(b => b.BrandName == BrandSearchString);
            }

            if (!String.IsNullOrEmpty(ModelSearchString))
            {
                cars = cars.Where(b => b.DegemName == ModelSearchString);
            }*/

            return View(carList);
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
