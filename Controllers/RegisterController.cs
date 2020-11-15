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
        //public Database_PorridgeSellingManagementStore_v1_1Entities _db = new Database_PorridgeSellingManagementStore_v1_1Entities();
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
                @ViewBag.Message1 = "Tên đăng nhập bị trùng, vui lòng sử dụng tên khác";
                return View();
            }
            if (ModelState.IsValid)
            {
                var querryUsersCount = from User in _db.Users
                                       select User.IDUser;

                model.IDUser = "U" + querryUsersCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                EncryptionPW encryptionPW = new EncryptionPW(model.Password);
                model.Password = encryptionPW.EncryptPass();
                _db.Users.Add(model);
                //Setting Default: Role "Customer" in Register page
                User_Roles user_role = new User_Roles();
                user_role.IDRole = "R03";
                user_role.IDUser = model.IDUser;
                _db.User_Roles.Add(user_role);
                _db.SaveChanges();
                @ViewBag.Message = "Successful";
            }
            return View();
        }
    }
}