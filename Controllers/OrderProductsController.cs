using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Web.Models;

namespace Project_Web.Controllers
{
    public class OrderProductsController : Controller
    {
        Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();
        // GET: OrderProducts
        [Authorize_userController]
        public ActionResult OrderProducts()
        {
            return View();
        }
        [HttpGet]
        [Authorize_userController]
        public ActionResult Shipping()
        {
            return View();
        }
        [HttpPost()]
        [Authorize_userController]
        public ActionResult Shipping(FormCollection form)
        {
            User user = new User();
            user = Session["User"] as User;
            Address_Users address_Users = new Address_Users();
            address_Users.IDUser = user.IDUser;
            address_Users.IsDefault = 0;
            address_Users.Fullname = form["FullName"].ToString();
            address_Users.PhoneNumber = form["PhoneNumber"].ToString();
            address_Users.Province = form["Province"].ToString();
            address_Users.District = form["District"].ToString();
            address_Users.Ward = form["Ward"].ToString();
            address_Users.Street = form["Street"].ToString();
            var querryaddress_UsersCount = (from au in _db.Address_Users
                                   select au).ToList();
            address_Users.IDAddress = "A-U" + querryaddress_UsersCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
            _db.Address_Users.Add(address_Users);
            _db.SaveChanges();
            return Content("true");
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
                                 select new { d.Name, d.Type}).ToList();
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