using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatchAndEnjoy.Models;

namespace WatchAndEnjoy.Controllers
{
    public class MoviesController : Controller
    {
        private WatchAndEnjoyDbEntities db = new WatchAndEnjoyDbEntities();

        // GET: Movies
        public ActionResult Index(string search, int page = 0)
        {
            var movies = from pr in db.Movies select pr;
            //Paging part -----------------------------------------------------------
            const int PageSize = 3; // you can always do something more elegant to set this
            var count = db.Movies.Count();
            
            movies = db.Movies.OrderBy(o => o.Id).Skip(page * PageSize).Take(PageSize);

            this.ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);
            this.ViewBag.Page = page;
            //paging part -------------------------------------------------------------

            //search part -------------------------------------------------------------
            if (!String.IsNullOrEmpty(search))
            {
                movies = db.Movies.Where(m => m.Name.Contains(search) || m.Name.Contains(search));
            }
            //search part ------------------------------------------------------------
            return View(movies.ToList());
        }

        //Search Function
        public ActionResult getMovie(string term)
        {
            Index(term);
            return Json(db.Movies.Where(c => c.Name.StartsWith(term)).Select(a => new { label = a.Name }), JsonRequestBehavior.AllowGet);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Genre,ReleaseYear,Rating,Cast,Price,Id")] Movy movy)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movy);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movy movy = db.Movies.Find(id);
            if (movy == null)
            {
                return HttpNotFound();
            }
            return View(movy);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Genre,ReleaseYear,Rating,Cast,Price,Id")] Movy movy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movy);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movy movy = db.Movies.Find(id);
            if (movy == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteMovies", movy);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movy movy = db.Movies.Find(id);
            db.Movies.Remove(movy);
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
