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
    public class FavouriteFilmsController : Controller
    {
        private Entities db = new Entities();

        // GET: FavouriteFilms
        public ActionResult Index()
        {
            var favouriteFilms = db.FavouriteFilms.Include(f => f.Film);
            return View(favouriteFilms.ToList());
        }

        // GET: FavouriteFilms/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavouriteFilm favouriteFilm = db.FavouriteFilms.Find(id);
            if (favouriteFilm == null)
            {
                return HttpNotFound();
            }
            return View(favouriteFilm);
        }

        // GET: FavouriteFilms/Create
        public ActionResult Create()
        {
            ViewBag.filmId = new SelectList(db.Films, "id", "name");
            return View();
        }

        // POST: FavouriteFilms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userId,filmId,film_name,favouriteFilmId")] FavouriteFilm favouriteFilm)
        {
            string userId = User.Identity.GetUserId();
            favouriteFilm.userId = userId;
            favouriteFilm.film_name = "new Film";
            if (ModelState.IsValid)
            {
                db.FavouriteFilms.Add(favouriteFilm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.filmId = new SelectList(db.Films, "id", "name", favouriteFilm.filmId);
            return View(favouriteFilm);
        }

        // GET: FavouriteFilms/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavouriteFilm favouriteFilm = db.FavouriteFilms.Find(id);
            if (favouriteFilm == null)
            {
                return HttpNotFound();
            }
            ViewBag.filmId = new SelectList(db.Films, "id", "name", favouriteFilm.filmId);
            return View(favouriteFilm);
        }

        // POST: FavouriteFilms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userId,filmId,film_name,favouriteFilmId")] FavouriteFilm favouriteFilm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favouriteFilm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.filmId = new SelectList(db.Films, "id", "name", favouriteFilm.filmId);
            return View(favouriteFilm);
        }

        // GET: FavouriteFilms/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavouriteFilm favouriteFilm = db.FavouriteFilms.Find(id);
            if (favouriteFilm == null)
            {
                return HttpNotFound();
            }
            return View(favouriteFilm);
        }

        // POST: FavouriteFilms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            FavouriteFilm favouriteFilm = db.FavouriteFilms.Find(id);
            db.FavouriteFilms.Remove(favouriteFilm);
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
