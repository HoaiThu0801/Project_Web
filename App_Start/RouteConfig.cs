using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project_Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute(
            //  name: "Add Cart",
            //  url: "them-gio-hang",
            //  defaults: new { controller = "Home", action = "AddCart", id = UrlParameter.Optional },
            //  namespaces: new[] { "Project_Web.Controllers" }
            // );
            //routes.MapRoute(
            // name: "Delete Cart",
            // url: "delete-cart",
            // defaults: new { controller = "Home", action = "DeleteCart", id = UrlParameter.Optional },
            // namespaces: new[] { "Project_Web.Controllers" }
            //);
            routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
             );
            routes.MapRoute(
           name: "Cart",
           url: "{controller}/{action}/{id}",
           defaults: new { controller = "Home", action = "ShoppingCart", id = UrlParameter.Optional },
           namespaces: new[] { "Project_Web.Controllers" }
          );
            routes.MapRoute(
         name: "SignIn",
         url: "{controller}/{action}/{id}",
         defaults: new { controller = "SignIn", action = "SignIn", id = UrlParameter.Optional },
         namespaces: new[] { "Project_Web.Controllers" }
        );
            routes.MapRoute(
        name: "OrderManagement",
        url: "{controller}/{action}/{id}",
        defaults: new { controller = "OrderManagement", action = "OrderManagement", id = UrlParameter.Optional },
        namespaces: new[] { "Project_Web.Controllers" }
       );
        }
    }
}
