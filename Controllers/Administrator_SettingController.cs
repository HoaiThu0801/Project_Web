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
        public ActionResult CreateSeller(CreateSeller model)
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
                User usertemp = new User();
                //Add user into "User" table
                var querryUsersCount = from User in _db.Users
                                       select User.IDUser;
                usertemp.IDUser = "U" + querryUsersCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);

                EncryptionPW encryptionPW = new EncryptionPW(model.Password);
                model.Password = encryptionPW.EncryptPass();
                usertemp.Username = model.Username;
                usertemp.Password = model.Password;
                usertemp.Fullname = model.Fullname;
                usertemp.Address = model.Address;
                usertemp.PhoneNumber = model.PhoneNumber;
                usertemp.Email = model.Email;
                _db.Users.Add(usertemp);


                //Add role of User into "User_Role" table

                User_Roles User_Role = new User_Roles();
                string roletemp = model.Role;
                User_Role.IDUser = usertemp.IDUser;
                var querryGetRole = (from role in _db.Roles
                                     where role.Role1 == roletemp
                                     select role.IDRole).SingleOrDefault();
                User_Role.IDRole = querryGetRole.ToString();
                _db.User_Roles.Add(User_Role);
                _db.SaveChanges();
                @ViewBag.Message = "Thành công";
                return Content("true");
            }


            return Content("false");
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
        #endregion

        #region Create Dish
        [HttpGet]
        public ActionResult CreateDish()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDish(string DishName, string Ingredient, string ImportPrice, string SalePrice, string Image, string Category)
        {

            Menu menu_temp = new Menu();
            var querryDishesCount = from Menu in _db.Menus
                                    select Menu.IDDish;
            Menu menu = _db.Menus.SingleOrDefault(n => n.DishName == DishName);
            if (menu != null)
            {
                return Content("false");
            }
            if (Category == "")
            {
                return Content("CategoryNull");
            }
            if (Image == "/images/ImageProducts/undefined")
            {
                return Content("ImageNull");
            }
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
                return Content("true");
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
                return Content("False");
            }
            Menu_Stores menu_store = new Menu_Stores();
            menu_store.IDDish = dish.IDDish;
            menu_store.IDStore = store.IDStore;
            menu_store.Ingredient = dish.Ingredient;
            if (menu_store != null)
            {
                _db.Menu_Stores.Add(menu_store);
                _db.SaveChanges();
                @ViewBag.Message = dishname + " được thêm vào thành công";
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
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    if(!(System.IO.Directory.Exists(targetpath)))
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
                                if(m.DishName == null)
                                    data.Add("Dữ liệu của trường DishName trong excel đang trống\n");
                                if(m.Ingredient == null)
                                    data.Add("Dữ liệu của trường Ingredient trong excel đang trống\n");
                                if (m.ImportPrice == null)
                                    data.Add("Dữ liệu của trường ImportPrice trong excel đang trống\n");
                                if (m.SalePrice == null)
                                    data.Add("Dữ liệu của trường SalePrice trong excel đang trống\n");
                                if (m.Image == null)
                                    data.Add("Dữ liệu của trường Image trong excel đang trống\n");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
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
                    data.Add("Thêm dữ liệu thành công");
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("Vui lòng chọn tệp excel");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("Vui lòng chọn tệp để thêm vào");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}