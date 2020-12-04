using Project_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using Microsoft.SqlServer.Server;

namespace Project_Web.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User model)
        {
            User user = _db.Users.SingleOrDefault(n => n.Username == model.Username);
            if (user != null)
            {
                return Content("user");
            }
            user = _db.Users.SingleOrDefault(n => n.Email == model.Email);
            if (user != null)
            {
                return Content("email");
            }
            if (ModelState.IsValid)
            {
                var querryUsersCount = from User in _db.Users
                                       select User.IDUser;
                
                model.IDUser = "U" + querryUsersCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                EncryptionPW encryptionPW = new EncryptionPW(model.Password);
                model.Password = encryptionPW.EncryptPass();
                model.Image = "Null";
                model.Facebook = "0";
                _db.Users.Add(model);
                //Setting Default: Role "Customer" in Register page
                User_Roles user_role = new User_Roles();
                user_role.IDRole = "R03";
                user_role.IDUser = model.IDUser;
                _db.User_Roles.Add(user_role);
                _db.SaveChanges();
                @ViewBag.Message = "Successful";
                return Content("true");
            }
            else
            {
                return Content("false");
            }
            
        }
    }
}