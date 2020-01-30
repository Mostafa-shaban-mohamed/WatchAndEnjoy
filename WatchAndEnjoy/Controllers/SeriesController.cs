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
    [AllowAnonymous]
    public class SeriesController : Controller
    {
        private WatchAndEnjoyDbEntities db = new WatchAndEnjoyDbEntities();

        // GET: Series
        public ActionResult Index(string search, int page = 0)
        {
            var series = from pr in db.Series select pr;
            //Paging part -----------------------------------------------------------
            const int PageSize = 3; // you can always do something more elegant to set this
            var count = db.Series.Count();

            series = db.Series.OrderBy(o => o.Id).Skip(page * PageSize).Take(PageSize);

            this.ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);
            this.ViewBag.Page = page;
            //paging part -------------------------------------------------------------

            //search part -------------------------------------------------------------
            if (!String.IsNullOrEmpty(search))
            {
                series = db.Series.Where(m => m.Name.Contains(search) || m.Name.Contains(search));
            }
            //search part ------------------------------------------------------------
            return View(series.ToList());
        }

        //Search Function
        public ActionResult getSerie(string term)
        {
            Index(term);
            return Json(db.Series.Where(c => c.Name.StartsWith(term)).Select(a => new { label = a.Name }), JsonRequestBehavior.AllowGet);
        }

        // GET: Series/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Series/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Genre,ReleaseYear,No_ofSeasons,No_ofEps,Rating,Cast,Price,Id")] Series series)
        {
            if (ModelState.IsValid)
            {
                db.Series.Add(series);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(series);
        }

        // GET: Series/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Series.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        // POST: Series/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Genre,ReleaseYear,No_ofSeasons,No_ofEps,Rating,Cast,Price,Id")] Series series)
        {
            if (ModelState.IsValid)
            {
                db.Entry(series).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(series);
        }

        // GET: Series/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Series.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteSeries",series);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Series series = db.Series.Find(id);
            db.Series.Remove(series);
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
