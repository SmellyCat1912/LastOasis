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
    public class WishListsController : Controller
    {
        private Entities db = new Entities();

        // GET: WishLists
        public ActionResult Index()
        {
            var wishLists = db.WishLists.Include(w => w.Film);
            return View(wishLists.ToList());
        }

        // GET: WishLists/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishList wishList = db.WishLists.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            return View(wishList);
        }

        // GET: WishLists/Create
        public ActionResult Create()
        {
            ViewBag.filmId = new SelectList(db.Films, "id", "name");
            return View();
        }

        // POST: WishLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userId,filmId,filmName,wishId")] WishList wishList)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                wishList.userId = userId;
                wishList.filmName = "new Film";
                db.WishLists.Add(wishList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.filmId = new SelectList(db.Films, "id", "name", wishList.filmId);
            return View(wishList);
        }

        // GET: WishLists/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishList wishList = db.WishLists.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            ViewBag.filmId = new SelectList(db.Films, "id", "name", wishList.filmId);
            return View(wishList);
        }

        // POST: WishLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userId,filmId,filmName,wishId")] WishList wishList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.filmId = new SelectList(db.Films, "id", "name", wishList.filmId);
            return View(wishList);
        }

        // GET: WishLists/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishList wishList = db.WishLists.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            return View(wishList);
        }

        // POST: WishLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            WishList wishList = db.WishLists.Find(id);
            db.WishLists.Remove(wishList);
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
