using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TheGamerR00M
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index", }
            );

            routes.MapRoute(
                name: "Home2",
                url: "Home",
                defaults: new { controller = "Home", action = "Index", }
            );

            routes.MapRoute(
                name: "Reviews",
                url: "Reviews",
                defaults: new { controller = "Posts", action = "Reviews", }
            );

            routes.MapRoute(
                name: "Stories",
                url: "Stories",
                defaults: new { controller = "Posts", action = "Stories", }
            );

            routes.MapRoute(
                name: "NewPost",
                url: "NewPost",
                defaults: new { controller = "Posts", action = "NewPost", }
            );

            routes.MapRoute(
                name: "SavePost",
                url: "SavePost",
                defaults: new { controller = "Posts", action = "SavePost", }
            );

            routes.MapRoute(
                name: "gotoPost",
                url: "{StoryOrReview}/{postID}",
                defaults: new { controller = "Posts", action = "gotoPost"}
            );

            routes.MapRoute(
                name: "LogIn",
                url: "Login",
                defaults: new { controller = "LogIn", action = "Index", }
            );

            routes.MapRoute(
                name: "MyAccount",
                url: "MyAccount",
                defaults: new { controller = "Account", action = "Index", }
            );

            routes.MapRoute(
                name: "PageNotFound",
                url: "Oops",
                defaults: new { controller = "Home", action = "PageNotFound", }
            );

            routes.MapRoute(
                name: "CatchAll",
                url: "{*any}",
                defaults: new { controller = "Home", action = "Error", }
            );

            routes.MapRoute(
                name: "Default1",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "index", id = UrlParameter.Optional }
            );
        }
    }
}
