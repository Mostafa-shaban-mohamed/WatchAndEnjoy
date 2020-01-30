using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using WatchAndEnjoy.Models;

namespace WatchAndEnjoy.Controllers
{
    public class CustomerController : Controller
    {
        private WatchAndEnjoyDbEntities db = new WatchAndEnjoyDbEntities();

        // GET: Customer/Index_Movies
        public ActionResult Index_Movies(string SortingOrder,string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = SortingOrder;
            ViewBag.SortingByName = String.IsNullOrEmpty(SortingOrder) ? "Name" : "";
            ViewBag.SortingByRating = String.IsNullOrEmpty(SortingOrder) ? "Rating" : "";
            ViewBag.SortingByGenre = String.IsNullOrEmpty(SortingOrder) ? "Genre" : "";
            ViewBag.SortingByPrice = String.IsNullOrEmpty(SortingOrder) ? "Price" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }
            ViewBag.FilterValue = Search_Data;

            var movie = from mov in db.Movies select mov;
            if(!String.IsNullOrEmpty(Search_Data))
            {
                movie = movie.Where(mov => mov.Name.Contains(Search_Data));
            }
            switch (SortingOrder)
            {
                case "Name":
                    movie = movie.OrderByDescending(mov => mov.Name);
                    break;
                case "Rating":
                    movie = movie.OrderBy(mov => mov.Rating);
                    break;
                case "Genre":
                    movie = movie.OrderBy(mov => mov.Genre);
                    break;
                case "Price":
                    movie = movie.OrderBy(mov => mov.Price);
                    break;
                default:
                    movie = movie.OrderBy(mov => mov.Name);
                    break;
            }
            int Size_Of_Page = 4;
            int No_Of_Page = (Page_No ?? 1);
            return View(movie.ToPagedList(No_Of_Page,Size_Of_Page));
        }

        // GET: Customer/Details_Movies/5
        public ActionResult Details_Movies(int? id)
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

        // Anime ------------------------------------------------------------------------------------

        // GET: Customer/Index_Animes
        public ActionResult Index_Animes(string SortingOrder, string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = SortingOrder;
            ViewBag.SortingByName = String.IsNullOrEmpty(SortingOrder) ? "Name" : "";
            ViewBag.SortingByRating = String.IsNullOrEmpty(SortingOrder) ? "Rating" : "";
            ViewBag.SortingByGenre = String.IsNullOrEmpty(SortingOrder) ? "Genre" : "";
            ViewBag.SortingByPrice = String.IsNullOrEmpty(SortingOrder) ? "Price" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }
            ViewBag.FilterValue = Search_Data;

            var anime = from ani in db.Animes select ani;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                anime = anime.Where(mov => mov.Name.Contains(Search_Data));
            }
            switch (SortingOrder)
            {
                case "Name":
                    anime = anime.OrderByDescending(mov => mov.Name);
                    break;
                case "Rating":
                    anime = anime.OrderBy(mov => mov.Rating);
                    break;
                case "Genre":
                    anime = anime.OrderBy(mov => mov.Genre);
                    break;
                case "Price":
                    anime = anime.OrderBy(mov => mov.Price);
                    break;
                default:
                    anime = anime.OrderBy(mov => mov.Name);
                    break;
            }
            int Size_Of_Page = 4;
            int No_Of_Page = (Page_No ?? 1);
            return View(anime.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        // GET: Customer/Details_Animes/5
        public ActionResult Details_Animes(int? id)
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

        // Series ---------------------------------------------------------------------------------

        // GET: Customer/Index_Series
        public ActionResult Index_Series(string SortingOrder, string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = SortingOrder;
            ViewBag.SortingByName = String.IsNullOrEmpty(SortingOrder) ? "Name" : "";
            ViewBag.SortingByRating = String.IsNullOrEmpty(SortingOrder) ? "Rating" : "";
            ViewBag.SortingByGenre = String.IsNullOrEmpty(SortingOrder) ? "Genre" : "";
            ViewBag.SortingByPrice = String.IsNullOrEmpty(SortingOrder) ? "Price" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }
            ViewBag.FilterValue = Search_Data;

            var serie = from ser in db.Series select ser;
            if (!String.IsNullOrEmpty(Search_Data))
            {
                serie = serie.Where(mov => mov.Name.Contains(Search_Data));
            }
            switch (SortingOrder)
            {
                case "Name":
                    serie = serie.OrderByDescending(mov => mov.Name);
                    break;
                case "Rating":
                    serie = serie.OrderBy(mov => mov.Rating);
                    break;
                case "Genre":
                    serie = serie.OrderBy(mov => mov.Genre);
                    break;
                case "Price":
                    serie = serie.OrderBy(mov => mov.Price);
                    break;
                default:
                    serie = serie.OrderBy(mov => mov.Name);
                    break;
            }
            int Size_Of_Page = 4;
            int No_Of_Page = (Page_No ?? 1);
            return View(serie.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        // GET: Customer/Details_Animes/5
        public ActionResult Details_Series(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series serie = db.Series.Find(id);
            if (serie == null)
            {
                return HttpNotFound();
            }
            return View(serie);
        }

        // Customers ----------------------------------------------------------------------------------------------

        // Registeration
        public ActionResult Register()
        {
            return View();
        }

        // POST: Customer/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Name,BirthDate,Email,Id")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(customer);
        }

        //Log-In --------------------->
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Customer model)
        {
            if (ModelState.IsValid)
            {
                using (WatchAndEnjoyDbEntities db = new WatchAndEnjoyDbEntities())
                {
                    Customer user = db.Customers
                                       .Where(u => u.ID == model.ID && u.Password == model.Password)
                                       .FirstOrDefault();

                    if (user != null)
                    {
                        Session["Name"] = user.Name;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid User Name or Password");
                        return View(model);
                    }
                }
            }
            else
            {
                return View(model);
            }
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
