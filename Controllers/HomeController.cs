using Project_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Project_Web.Controllers
{
    //[AuthorizeController]
    public class HomeController : Controller
    {
        public Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();
        public ActionResult Index(int ?page)
        {
            if (page == null) page = 1;
            var menu = (from l in _db.Menus
                         select l).OrderBy(x => x.IDDish);
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(menu.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Logout()
        {
            Session["Is Login"] = 0;
            Session.Remove("User");
            return RedirectToAction("Index", "Home");
        }
    }
}