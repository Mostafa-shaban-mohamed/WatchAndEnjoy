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
    
    public class AnimesController : Controller
    {
        private WatchAndEnjoyDbEntities db = new WatchAndEnjoyDbEntities();

        // GET: Animes
        public ActionResult Index(string search, int page = 0)
        {
            var animes = from pr in db.Animes select pr;
            //Paging part -----------------------------------------------------------
            const int PageSize = 3; // you can always do something more elegant to set this
            var count = db.Animes.Count();

            animes = db.Animes.OrderBy(o => o.Id).Skip(page * PageSize).Take(PageSize);

            this.ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);
            this.ViewBag.Page = page;
            //paging part -------------------------------------------------------------

            //search part -------------------------------------------------------------
            if (!String.IsNullOrEmpty(search))
            {
                animes = db.Animes.Where(m => m.Name.Contains(search) || m.Name.Contains(search));
            }
            //search part ------------------------------------------------------------
            return View(animes.ToList());
        }

        //Search Function
        public ActionResult getAnime(string term)
        {
            Index(term);
            return Json(db.Animes.Where(c => c.Name.StartsWith(term)).Select(a => new { label = a.Name }), JsonRequestBehavior.AllowGet);
        }

        // GET: Animes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Animes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Genre,ReleaseYear,No_ofSeasons,No_ofEps,Rating,Cast,Price,Id")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                db.Animes.Add(anime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anime);
        }

        // GET: Animes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anime anime = db.Animes.Find(id);
            if (anime == null)
            {
                return HttpNotFound();
            }
            return View(anime);
        }

        // POST: Animes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Genre,ReleaseYear,No_ofSeasons,No_ofEps,Rating,Cast,Price,Id")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anime);
        }

        // GET: Animes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anime anime = db.Animes.Find(id);
            if (anime == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteAnimes",anime);
        }

        // POST: Animes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anime anime = db.Animes.Find(id);
            db.Animes.Remove(anime);
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
