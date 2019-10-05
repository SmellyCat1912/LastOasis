using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LastOasis.Models;
using Microsoft.AspNet.Identity;

namespace LastOasis.Controllers
{
    [Authorize(Roles = "user")]
    public class PurchasingsController : Controller
    {
      
        private Entities db = new Entities();

        // GET: Purchasings
        public ActionResult Index()
        {
            var purchasings = db.Purchasings.Include(p => p.Film);
            return View(purchasings.ToList());
        }

        // GET: Purchasings/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchasing purchasing = db.Purchasings.Find(id);
            if (purchasing == null)
            {
                return HttpNotFound();
            }
            return View(purchasing);
        }

        // GET: Purchasings/Create
        public ActionResult Create()
        {
            ViewBag.filmId = new SelectList(db.Films, "id", "name");
            return View();
        }

        // POST: Purchasings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userId,filmId,userName,filmName,qty,date,purchasingId")] Purchasing purchasing)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                purchasing.userId = userId;
                purchasing.filmName = "new Film";
                purchasing.userName = "new user";
                db.Purchasings.Add(purchasing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.filmId = new SelectList(db.Films, "id", "name", purchasing.filmId);
            return View(purchasing);
        }

        // GET: Purchasings/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchasing purchasing = db.Purchasings.Find(id);
            if (purchasing == null)
            {
                return HttpNotFound();
            }
            ViewBag.filmId = new SelectList(db.Films, "id", "name", purchasing.filmId);
            return View(purchasing);
        }

        // POST: Purchasings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userId,filmId,userName,filmName,qty,date,purchasingId")] Purchasing purchasing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchasing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.filmId = new SelectList(db.Films, "id", "name", purchasing.filmId);
            return View(purchasing);
        }

        // GET: Purchasings/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchasing purchasing = db.Purchasings.Find(id);
            if (purchasing == null)
            {
                return HttpNotFound();
            }
            return View(purchasing);
        }

        // POST: Purchasings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Purchasing purchasing = db.Purchasings.Find(id);
            db.Purchasings.Remove(purchasing);
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
