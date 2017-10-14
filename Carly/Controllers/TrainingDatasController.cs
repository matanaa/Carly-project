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
    public class TrainingDatasController : Controller
    {
        private CarlyContext db = new CarlyContext();

        // GET: TrainingDatas
        public ActionResult Index()
        {
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }
            return View(db.TrainingDatas.ToList());
        }

        public ActionResult LoadFromFile()
        {
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }
            var fileContents = System.IO.File.ReadAllText(Server.MapPath("~/Content/load.csv"));
            foreach (var line in fileContents.Split('\n'))
            {

                var arr = line.ToString().Split('~');
                TrainingData tmp = new TrainingData();
                if (arr.Length==2){ 
                tmp.title = arr[0];
                tmp.word = arr[1];
                db.TrainingDatas.Add(tmp);
            }
                //db.SaveChanges();

            }
            db.SaveChanges();

            return Content(fileContents);

            //return View(db.TrainingDatas.ToList());
        }
        public ActionResult goodList()
        {
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }
            return View("Index", db.TrainingDatas.Where(g => g.title.Equals("good")).ToList());
            
        }

        // GET: TrainingDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingData trainingData = db.TrainingDatas.Find(id);
            if (trainingData == null)
            {
                return HttpNotFound();
            }
            return View(trainingData);
        }

        // GET: TrainingDatas/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }
            return View();
        }

        // POST: TrainingDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,word")] TrainingData trainingData)
        {
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }
            if (ModelState.IsValid)
            {
                db.TrainingDatas.Add(trainingData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trainingData);
        }

        // GET: TrainingDatas/Edit/5
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
            TrainingData trainingData = db.TrainingDatas.Find(id);
            if (trainingData == null)
            {
                return HttpNotFound();
            }
            return View(trainingData);
        }

        // POST: TrainingDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,word")] TrainingData trainingData)
        {
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }
            if (ModelState.IsValid)
            {
                db.Entry(trainingData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainingData);
        }

        // GET: TrainingDatas/Delete/5
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
            TrainingData trainingData = db.TrainingDatas.Find(id);
            if (trainingData == null)
            {
                return HttpNotFound();
            }
            return View(trainingData);
        }

        // POST: TrainingDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }
            TrainingData trainingData = db.TrainingDatas.Find(id);
            db.TrainingDatas.Remove(trainingData);
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
