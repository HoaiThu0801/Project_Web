using Project_Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.UI;

namespace Project_Web.Controllers
{
    [AuthorizeController]
    public class Administrator_SettingController : Controller
    {

        public Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();
        // GET: Administrator_Setting
        [HttpGet]
        public ActionResult CreateSeller()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateSeller(CreateSeller model)
        {
            User usertemp = _db.Users.SingleOrDefault(n => n.Username == model.Username);
            if (usertemp != null)
            {
                @ViewBag.Message1 = "Tên đăng nhập bị trùng, vui lòng sử dụng tên khác";
                return View();
            }

            if (ModelState.IsValid) 
            {
                User user = new User();
                //Add user into "User" table
                var querryUsersCount = from User in _db.Users
                                       select User.IDUser;
                user.IDUser = "U" + querryUsersCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                EncryptionPW encryptionPW = new EncryptionPW(model.Password);
                model.Password = encryptionPW.EncryptPass();
                user.Username = model.Username;
                user.Password = model.Password;
                user.Fullname = model.Fullname;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;

                _db.Users.Add(user);

                //Add role of User into "User_Role" table

                User_Roles User_Role = new User_Roles();
                string roletemp =model.Role;
                User_Role.IDUser = user.IDUser;
                var querryGetRole = (from role in _db.Roles
                                     where role.Role1 == roletemp
                                     select role.IDRole).SingleOrDefault();
                User_Role.IDRole = querryGetRole.ToString();
                _db.User_Roles.Add(User_Role);
                _db.SaveChanges();
                @ViewBag.Message = "Thành công";
                return Content("true");
            }


            return View();
        }
        [HttpGet]
        public ActionResult CreateStore()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateStore(Store model)
        {
            var querryStoresCount = from Store in _db.Stores
                                   select Store.IDStore;
            string errorMessage = null;
            model.IDStore = "S" + querryStoresCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
            if (ModelState.IsValid)
            {
                _db.Stores.Add(model);
                //Setting Default: While creating store, creating warehouse of store
                Warehouse warehouse = new Warehouse();
                warehouse.IDWarehouse = "WH" + querryStoresCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                warehouse.WarehouseName = "Kho hàng " + model.StoreName;
                warehouse.LocationofWarehouse = model.Location;
                warehouse.IDStore = model.IDStore;
                _db.Warehouses.Add(warehouse);
                _db.SaveChanges();
                return Content("true");
            }
            if (!ModelState.IsValidField("StoreName"))
            {
                errorMessage = "StoreName";
                return Content(errorMessage);
            }
            if (!ModelState.IsValidField("PhoneNumber"))
            {
                errorMessage = "PhoneNumber";
                return Content(errorMessage);
            }
            if (!ModelState.IsValidField("Email"))
            {
                errorMessage = "Email";
                return Content(errorMessage);
            }
            return Content("errormessage");
        }
        [HttpPost]
        public ActionResult CreateDish(Store model)
        {
            var querryStoresCount = from Store in _db.Stores
                                    select Store.IDStore;
            model.IDStore = "S" + querryStoresCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
            if (ModelState.IsValid)
            {
                _db.Stores.Add(model);
                //Setting Default: While creating store, creating warehouse of store
                Warehouse warehouse = new Warehouse();
                warehouse.IDWarehouse = "WH" + querryStoresCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                warehouse.WarehouseName = "Kho hàng " + model.StoreName;
                warehouse.LocationofWarehouse = model.Location;
                warehouse.IDStore = model.IDStore;
                _db.Warehouses.Add(warehouse);
                _db.SaveChanges();
                @ViewBag.Message = "Successful";
            }
            return View();
        }
        [HttpGet]
        public JsonResult LoadStore()
        {
            var store = (from s in _db.Stores
                         select new
                         {
                             StoreName = s.StoreName,
                             Location = s.Location,
                             PhoneNumber = s.PhoneNumber,
                             Email = s.Email
                         });
            return Json(store.ToList(), JsonRequestBehavior.AllowGet); 
        }
        [HttpGet]
        public ActionResult CreateStaff ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateStaff(string Store, string Staff)
        {
            string storename = Store;
            string fullname = Staff;
            Store store = (from s in _db.Stores
                           where s.StoreName == storename
                           select s).FirstOrDefault();
            string user = (from u in _db.Users
                         where u.Fullname == fullname
                           select u.IDUser).FirstOrDefault();
            if (store != null && user != null)
            {
                store.IDUser = user;
                _db.Stores.AddOrUpdate(store);
                _db.SaveChanges();
            }
            
            return View();
        }

    }
}