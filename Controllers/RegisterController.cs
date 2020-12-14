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
        public ActionResult Register(User model, FormCollection form)
        {
            Address_Users address_Users = new Address_Users();
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
                var querryaddress_UsersCount = (from au in _db.Address_Users
                                                select au).ToList();
                model.IDUser = "U" + querryUsersCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                address_Users.IDAddress = "A-U" + querryaddress_UsersCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                address_Users.IDUser = model.IDUser;
                address_Users.Province = form["Province"].ToString();
                address_Users.District = form["District"].ToString();
                address_Users.Ward = form["Ward"].ToString();
                address_Users.IsDefault = 1;
                address_Users.Street = form["Address"].ToString();
                address_Users.PhoneNumber = model.PhoneNumber;
                address_Users.Fullname = model.Fullname;
                EncryptionPW encryptionPW = new EncryptionPW(model.Password);
                model.Password = encryptionPW.EncryptPass();
                model.Image = "Null";
                model.Facebook = "0";
                _db.Users.Add(model);
                _db.Address_Users.Add(address_Users);
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

        #region LoadData
        [HttpGet]
        public JsonResult LoadDistrict(string ProvinceName)
        {
            Province pro = _db.Provinces.SingleOrDefault(n => n.Name == ProvinceName);
            if (pro != null)
            {
                List<string> namedistricts = new List<string>();
                string name;
                var districts = (from d in _db.Districts
                                 where d.ProvinceId == pro.Id
                                 select new { d.Name, d.Type }).ToList();
                foreach (var d in districts)
                {
                    name = d.Type + " " + d.Name;
                    namedistricts.Add(name);
                }
                return Json(namedistricts, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadWard(string DistrictName)
        {
            District dis = _db.Districts.SingleOrDefault(n => n.Name == DistrictName);
            if (dis != null)
            {
                List<string> nameward = new List<string>();
                string name;
                var wards = (from w in _db.Wards
                             where w.DistrictID == dis.Id
                             select new { w.Name, w.Type }).ToList();
                foreach (var w in wards)
                {
                    name = w.Type + " " + w.Name;
                    nameward.Add(name);
                }
                return Json(nameward, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}