using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WatchAndEnjoy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "CustomerMovies",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Customer", action = "Index_Movies", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "CustomerMoviesDetalis",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Customer", action = "Details_Movies", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "CustomerAnimes",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Customer", action = "Index_Animes", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "CustomerAnimesDetalis",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Customer", action = "Details_Animes", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "CustomerSeries",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Customer", action = "Index_Series", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "CustomerSeriesDetalis",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Customer", action = "Details_Series", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
