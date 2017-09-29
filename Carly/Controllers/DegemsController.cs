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
        public ActionResult Search(string BrandSearchString, string ColorSearchString, string ModelSearchString)
        {
            var degemList = new List<Brand>();

            // find all brand in the database
            // for the droplinkbutton
            var DegemQry = from s in db.Brands
                           orderby s.BrandName
                           select s; 
            degemList.AddRange(DegemQry.Distinct());
            ViewBag.degemList= degemList;

            //get the data to table
            var degems = from b in db.Degems select b; //TODO: change to join query
            if (!String.IsNullOrEmpty(ColorSearchString))
            {
                degems = degems.Where(b => b.Color.Contains(ColorSearchString));
            }

            if (!String.IsNullOrEmpty(BrandSearchString))
            {
                degems = degems.Where(b => b.BrandID.ToString() == BrandSearchString);
            }

            if (!String.IsNullOrEmpty(ModelSearchString))
            {
                degems = degems.Where(b => b.DegemName == ModelSearchString);
            }

            return View(degems);
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
        public ActionResult Create([Bind(Include = "DegemId,DegemName,Color,BrandID,Quantity")] Degem degem)
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
        public ActionResult Edit([Bind(Include = "DegemId,DegemName,Color,BrandID,Quantity")] Degem degem)
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
