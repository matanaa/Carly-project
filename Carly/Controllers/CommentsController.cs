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
    public class CommentsController : Controller
    {
        private CarlyContext db = new CarlyContext();

        // GET: Comments
        public ActionResult Index()
        {
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }
            var comment = db.Comment.Include(c => c.CommentDegem);
            return View(comment.ToList());
        }

        // GET: Comments/Details/5
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
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.DegemID = new SelectList(db.Degems, "DegemId", "DegemName");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DegemID,ContentInfo")] Comment comment)
        {
            comment.Author = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Comment.Add(comment);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return Redirect("/Degems/Details/"+comment.DegemID+"/#post-" + comment.ID);
            ViewBag.DegemID = new SelectList(db.Degems, "DegemId", "DegemName", comment.DegemID);
            return View(comment);
        }

        // GET: Comments/Edit/5
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
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DegemID = new SelectList(db.Degems, "DegemId", "DegemName", comment.DegemID);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DegemID,Author,ContentInfo")] Comment comment)
        {
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DegemID = new SelectList(db.Degems, "DegemId", "DegemName", comment.DegemID);
            return View(comment);
        }

        // GET: Comments/Delete/5
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
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }
            Comment comment = db.Comment.Find(id);
            db.Comment.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index", "Degems");
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
