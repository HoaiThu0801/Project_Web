using Project_Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using System.Data;
using LinqToExcel;

namespace Project_Web.Controllers
{
    [AuthorizeController]
    public class Administrator_SettingController : BaseController
    {

        public Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();
        // GET: Administrator_Setting

        #region CreateSeller
        [HttpGet]
        public ActionResult CreateSeller()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateSeller(CreateSeller model, FormCollection form)
        {
            User user = _db.Users.SingleOrDefault(n => n.Username == model.Username);
            User email = _db.Users.SingleOrDefault(n => n.Email == model.Email);
            var province = form["Province"];
            var district = form["District"];
            var ward = form["Ward"];
            var re_password = form["re_password"];

            //Validate
            if (user is null && email is null)
            {
                if (re_password != model.Password)
                    return Content("password");
                else
                {
                    EncryptionPW encryptionPW = new EncryptionPW(model.Password);
                    model.Password = encryptionPW.EncryptPass();
                    if (ModelState.IsValid)
                    {
                        User usertemp = new User();
                        User_Roles User_Role = new User_Roles();
                        Address_Users address_Users = new Address_Users();
                        if (province is null || district is null)
                        {
                            if (province is null)
                                return Content("province");
                            if (district is null)
                                return Content("district");
                        }
                        else
                        {
                            if (ward is null)
                                ward = "";
                            //Add user into "User" table
                            var querryUsersCount = from User in _db.Users
                                                   select User.IDUser;
                            usertemp.IDUser = "U" + querryUsersCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);

                            usertemp.Username = model.Username;
                            usertemp.Password = model.Password;
                            usertemp.Fullname = model.Fullname;
                            usertemp.Address = form["Address"].ToString();
                            usertemp.PhoneNumber = model.PhoneNumber;
                            usertemp.Email = model.Email;
                            usertemp.Image = "Null";
                            usertemp.Facebook = "0";
                            _db.Users.Add(usertemp);

                            //Add role of User into "User_Role" table
                            string roletemp = model.Role;
                            if (model.Role is null)
                            {
                                return Content("role");
                            }
                            else
                            {
                                User_Role.IDUser = usertemp.IDUser;
                                var querryGetRole = (from role in _db.Roles
                                                     where role.Role1 == roletemp
                                                     select role.IDRole).SingleOrDefault();
                                User_Role.IDRole = querryGetRole.ToString();
                                _db.User_Roles.Add(User_Role);
                                _db.SaveChanges();

                                //Add address default
                                var querryaddress_UsersCount = (from au in _db.Address_Users
                                                                select au).ToList();
                                address_Users.IDAddress = "A-U" + querryaddress_UsersCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                                address_Users.IDUser = usertemp.IDUser;
                                address_Users.Province = province;
                                address_Users.District = district;
                                address_Users.Ward = ward;
                                address_Users.IsDefault = 1;
                                address_Users.Street = form["Address"].ToString();
                                address_Users.PhoneNumber = model.PhoneNumber;
                                address_Users.Fullname = model.Fullname;
                                _db.Address_Users.Add(address_Users);
                                _db.SaveChanges();

                                return Content("true");
                            }
                        }
                    }
                    else
                    {
                        return Content("false");
                    }
                }
            }
            else
            {
                if (user != null)
                    return Content("user");
                else
                    return Content("email");
            }
            return View();
        }
        #endregion

        #region Create Store
        [HttpGet]
        public ActionResult CreateStore()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateStore(Store model)
        {
            Store email = _db.Stores.SingleOrDefault(n => n.Email == model.Email);
            string errorMessage = null;
            if (email is null)
            {
                var querryStoresCount = from Store in _db.Stores
                                        select Store.IDStore;
                model.IDStore = "S" + querryStoresCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                if (model.Location is null)
                {
                    return Content("nolocation");
                }
                else
                {
                    if (!ModelState.IsValidField("PhoneNumber"))
                    {
                        errorMessage = "PhoneNumber";
                        return Content(errorMessage);
                    }
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
                    return Content("errormessage");
                }
            }
            else
            {
                return Content("email");
            }
        }
        #endregion

        #region Create Dish
        [HttpGet]
        public ActionResult CreateDish()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDish(string DishName, string Ingredient, string ImportPrice, string SalePrice, string Category, HttpPostedFileBase FileUpload)
        {

            Menu menu_temp = new Menu();
            var querryDishesCount = from Menu in _db.Menus
                                    select Menu.IDDish;
            Menu menu = _db.Menus.SingleOrDefault(n => n.DishName == DishName);
            //Validate
            if (menu == null && Category != "null" && ImportPrice != "" && SalePrice != "" && FileUpload != null)
            {
                string filename = FileUpload.FileName;
                string targetpath = Server.MapPath("~/images/ImageProducts/");
                if (!(System.IO.Directory.Exists(targetpath)))
                {
                    System.IO.Directory.CreateDirectory(targetpath);
                }
                FileUpload.SaveAs(targetpath + filename);
                string Image = "images/ImageProducts/" + filename;
                menu_temp.IDDish = "D" + querryDishesCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                menu_temp.DishName = DishName;
                menu_temp.Ingredient = Ingredient;
                menu_temp.ImportPrice = Convert.ToDouble(ImportPrice);
                menu_temp.SalePrice = Convert.ToDouble(SalePrice);
                menu_temp.Image = Image;
                menu_temp.Category = Category;

                if (ModelState.IsValid)
                {
                    _db.Menus.Add(menu_temp);
                    _db.SaveChanges();
                    var success = new
                    {
                        title = "Thông báo",
                        message = "Tạo món ăn thành công",
                        type = true,
                    };
                    return Json(success, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                string error = "";
                if (menu != null)
                {
                    error = "menu";
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
                if (ImportPrice == "")
                {
                    error = "importPrice";
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
                if (SalePrice == "")
                {
                    error = "salePrice";
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
                if (FileUpload is null)
                {
                    error = "image";
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
                if (Category == "null")
                {
                    error = "category";
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }
        #endregion

        #region Create Staff
        [HttpGet]
        public ActionResult CreateStaff()
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
                return Content("true");
            }
            else
            {
                if (store is null)
                    return Content("nostore");
                if (user is null)
                    return Content("nouser");
            }
            return Content("false");
        }
        #endregion

        #region LoadData
        [HttpGet]
        public JsonResult LoadStaff()
        {
            var staff_store = (from s in _db.Stores
                               join u in _db.Users on s.IDUser equals u.IDUser
                               select new
                               {
                                   StoreName = s.StoreName,
                                   Location = s.Location,
                                   FullName = u.Fullname,
                                   UserName = u.Username
                               });
            return Json(staff_store.ToList(), JsonRequestBehavior.AllowGet);
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
        public JsonResult LoadStoreName(string Location)
        {
            var store = (from s in _db.Stores
                         where s.Location == Location
                         select new
                         {
                             StoreName = s.StoreName,
                         });
            return Json(store.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult LoadDish_no_StoreName(string StoreName)
        {
            Store store = _db.Stores.SingleOrDefault(n => n.StoreName == StoreName);
            var dish = (from m in _db.Menus
                        where !(_db.Menu_Stores.Any(ms => ms.IDDish == m.IDDish && ms.IDStore == store.IDStore))
                        select m.DishName);
            return Json(dish.ToList(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Paging
        public PartialViewResult _TabStore(int? page)
        {
            if (page == null) page = 1;
            var store = (from s in _db.Stores
                         select s).OrderBy(x => x.IDStore);
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return PartialView("_TabStore", store.ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult _TabStaff(int? page)
        {

            if (page == null) page = 1;
            var store = (from s in _db.Stores
                         where s.IDUser != null
                         select s).OrderBy(x => x.IDStore);
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return PartialView("_TabStaff", store.ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult _TabDish(int? page)
        {

            if (page == null) page = 1;
            var store = (from s in _db.Stores
                         select s).OrderBy(x => x.IDStore);
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return PartialView("_TabDish", store.ToPagedList(pageNumber, pageSize));
        }
        #endregion

        #region AddFoodintoStore
        [HttpPost]
        public ActionResult AddDish(string DishName, string StoreName, string Location)
        {
            string location = Location;
            string dishname = DishName;
            string storetemp = StoreName;
            var dish = (from d in _db.Menus
                        where d.DishName == dishname
                        select d).SingleOrDefault();
            var store = (from s in _db.Stores
                         where s.StoreName == storetemp && s.Location == location
                         select s).SingleOrDefault();
            Menu_Stores menu_store_temp = _db.Menu_Stores.SingleOrDefault(n => n.IDStore == store.IDStore && n.IDDish == dish.IDDish);

            if (menu_store_temp != null)
            {
                return Content("false");
            }
            Menu_Stores menu_store = new Menu_Stores();
            menu_store.IDDish = dish.IDDish;
            menu_store.IDStore = store.IDStore;
            menu_store.Ingredient = dish.Ingredient;
            if (menu_store != null)
            {
                _db.Menu_Stores.Add(menu_store);
                _db.SaveChanges();
                var success = new
                {
                    message = dishname,
                    type = true,
                };
                return Json(success, JsonRequestBehavior.AllowGet);
            }
            return View();
        }
        #endregion

        #region ExcelFile
        public FileResult DownLoadTemplate_Menu()
        {
            string path = "~/Templates/File_20201221_v1.0_TemplateImportProduct.xlsx";
            return File(path, "application/vnd.ms-excel", "File_20201221_v1.0_TemplateImportProduct.xlsx");
        }
        [HttpPost]
        public ActionResult UpLoadMenu(HttpPostedFileBase FileUpload)
        {
            string data = "";
            if (FileUpload != null)
            {
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    if (!(System.IO.Directory.Exists(targetpath)))
                    {
                        System.IO.Directory.CreateDirectory(targetpath);
                    }
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var MenuList = from a in excelFile.Worksheet<Menu>(sheetName) select a;
                    foreach (var m in MenuList)
                    {
                        try
                        {
                            if (m.DishName != null && m.Ingredient != null && m.ImportPrice != null && m.SalePrice != null && m.Image != null)
                            {
                                Menu dish = new Menu();
                                var querryDishesCount = from Menu in _db.Menus
                                                        select Menu.IDDish;
                                dish.IDDish = "D" + querryDishesCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                                dish.DishName = m.DishName;
                                dish.Category = m.Category;
                                dish.Ingredient = m.Ingredient;
                                dish.ImportPrice = m.ImportPrice;
                                dish.SalePrice = m.SalePrice;
                                dish.Image = m.Image;
                                _db.Menus.Add(dish);
                                _db.SaveChanges();
                            }
                            else
                            {
                                if (m.DishName == null)
                                {
                                    data = "dishname";
                                    return Json(data, JsonRequestBehavior.AllowGet);
                                }
                                if (m.Ingredient == null)
                                {
                                    data = "ingredient";
                                    return Json(data, JsonRequestBehavior.AllowGet);
                                }
                                if (m.ImportPrice == null)
                                {
                                    data = "importPrice";
                                    return Json(data, JsonRequestBehavior.AllowGet);
                                }
                                if (m.SalePrice == null)
                                {
                                    data = "salePrice";
                                    return Json(data, JsonRequestBehavior.AllowGet);
                                }
                                if (m.Image == null)
                                {
                                    data = "image";
                                    return Json(data, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                                }
                            }
                        }

                    }
                    data = "success";
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data = "warning";
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data = "false";
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}