using Project_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity.Migrations;

namespace Project_Web.Controllers
{
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
        [HttpGet]
        [Authorize_userController]
        public ActionResult InformationAccount()
        {
            return View();
        }
        [HttpPost]
        [Authorize_userController]
        public ActionResult InformationAccount(User model)
        {
            User user_session = Session["User"] as User;
            model.Username = user_session.Username;
            User user = (from u in _db.Users
                        where u.Username == model.Username
                        select u).SingleOrDefault();
            if (user != null)
            {
                user.Fullname = model.Fullname;
                user.Gender = model.Gender;
                user.DateofBirth = model.DateofBirth;
                user.IdentityCard = model.IdentityCard;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                _db.Users.AddOrUpdate(user);
                _db.SaveChanges();
                return View();

            }
          


            return View();
        }
        public ActionResult ChangePass()
        {
            return View();
        }
        [AuthorizeController]
        public ActionResult ShoppingCart()
        {
            return View();
        }
    }
}