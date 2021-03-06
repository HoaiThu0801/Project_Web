﻿using Project_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity.Migrations;
using System.Data;

namespace Project_Web.Controllers
{
    public class HomeController : BaseController
    {
        public Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();

        #region Paging
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var menu = (from l in _db.Menus
                        select l).OrderBy(x => x.IDDish);
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(menu.ToPagedList(pageNumber, pageSize));
        }
        #endregion

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

        #region Logout
        public ActionResult Logout()
        {
            Session["Is Login"] = 0;
            Session.Remove("User");
            Session.Remove("cart");
            SetAlert("Thông báo", "Bạn đã đăng xuất thành công", true);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region InformationAccount
        [HttpGet]
        [Authorize_userController]
        public ActionResult InformationAccount()
        {
            return View();
        }
        [HttpPost]
        [Authorize_userController]
        public ActionResult InformationAccount(string FullName, string Gender, string DateofBirth, string IdentityCard, string Province, string District, string Ward, string Street, string PhoneNumber, HttpPostedFileBase ImageAvatarUser)
        {
            User user_session = Session["User"] as User;
            Address_Users address_Users = _db.Address_Users.SingleOrDefault(x => x.IDUser == user_session.IDUser && x.IsDefault == 1);
            User user = (from u in _db.Users
                         where u.Username == user_session.Username
                         select u).SingleOrDefault();
            if (user != null)
            {
                if (ImageAvatarUser is null)
                {
                    user.Image = user.Image;
                }
                string filename = ImageAvatarUser.FileName;
                string targetpath = Server.MapPath("~/images/ImageAvatarUser/");
                if (!(System.IO.Directory.Exists(targetpath)))
                {
                    System.IO.Directory.CreateDirectory(targetpath);
                }
                ImageAvatarUser.SaveAs(targetpath + filename);
                string Image = "images/ImageAvatarUser/" + filename;
                if (address_Users != null)
                {
                    address_Users.Province = Province;
                    address_Users.District = District;
                    address_Users.Ward = Ward;
                    address_Users.Street = Street;
                    address_Users.Fullname = FullName;
                    address_Users.PhoneNumber = PhoneNumber;
                    _db.Address_Users.AddOrUpdate(address_Users);
                    _db.SaveChanges();
                }
                user.Fullname = FullName;
                user.Gender = Gender;
                user.DateofBirth = DateTime.Parse(DateofBirth);
                user.IdentityCard = IdentityCard;
                user.Address = Street;
                user.PhoneNumber = PhoneNumber;
                user.Image = Image;
                _db.Users.AddOrUpdate(user);
                _db.SaveChanges();
                Session["User"] = user;
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            return View();
        }
        #endregion

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

        #region ChangePass
        [Authorize_userController]
        [HttpGet]
        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(string Username, string OldPassword, string NewPassword, string Re_Password)
        {
            if (OldPassword == "error" || NewPassword == "error")
            {
                if (OldPassword == "error")
                    return Content("NotOldPassword");
                if (NewPassword == "error")
                    return Content("NotNewPassword");
            }
            else
            {
                if (OldPassword == NewPassword)
                {
                    return Content("IsEquals");
                }
                if (Re_Password != NewPassword)
                {
                    return Content("NotLike");
                }

                EncryptionPW encryptionOld_PW = new EncryptionPW(OldPassword);
                string old_password = encryptionOld_PW.EncryptPass();
                EncryptionPW encryptionNew_PW = new EncryptionPW(NewPassword);
                string new_password = encryptionNew_PW.EncryptPass();
                User user = _db.Users.SingleOrDefault(x => x.Username == Username && x.Password == old_password);
                if (user != null)
                {
                    user.Password = new_password;
                    _db.Users.AddOrUpdate(user);
                    _db.SaveChanges();
                    return Content("true");
                }
                else
                {
                    return Content("false");
                }
            }
            return View();
        }
        #endregion

        #region Cart
        [Authorize_userController]
        public ActionResult ShoppingCart()
        {
            return View();
        }

        [HttpPost]
        [Authorize_userController]
        public ActionResult AddCart(string IDDish)
        {
            User user = Session["User"] as User;
            if (user == null)
            {
                return Content("user");
            }
            string IDBill = "Default";
            var dish = (from s in _db.Menus
                        where s.IDDish == IDDish
                        select s).SingleOrDefault();
            if (dish != null)
            {
                DataTable cart = new DataTable();
                if (Session["cart"] == null)
                {
                    cart.Columns.Add("IDBillDetails");
                    cart.Columns.Add("DishName");
                    cart.Columns.Add("Price");
                    cart.Columns.Add("Quantity");
                    cart.Columns.Add("Promotion");
                    cart.Columns.Add("PaidPrice");
                    cart.Columns.Add("IDBill");
                    Session["cart"] = cart;
                }
                else
                {
                    cart = Session["cart"] as DataTable;
                    foreach (DataRow dr in cart.Rows)
                    {
                        IDBill = dr["IDBill"].ToString();
                        break;
                    }
                }
                bool isExisted = false;
                foreach (DataRow dr in cart.Rows)
                {
                    if (dr["DishName"].ToString() == dish.DishName)
                    {
                        dr["Quantity"] = (int.Parse(dr["Quantity"].ToString()) + 1).ToString();
                        dr["PaidPrice"] = (int.Parse(dr["Quantity"].ToString()) * float.Parse(dr["Price"].ToString())).ToString();
                        isExisted = true;
                        string IDBillDetail = dr["IDBillDetails"].ToString();
                        BillDetail billDetail = _db.BillDetails.SingleOrDefault(n => n.IDBillDetail == IDBillDetail);
                        if (billDetail != null)
                        {
                            billDetail.Quantity = int.Parse(dr["Quantity"].ToString());
                            billDetail.PaidPrice = float.Parse(dr["PaidPrice"].ToString());
                            _db.BillDetails.AddOrUpdate(billDetail);
                            _db.SaveChanges();
                        }
                        break;
                    }
                }
                if (isExisted == false)
                {
                    DataRow dr = cart.NewRow();
                    var querryIDBillDetailCount = from bd in _db.BillDetails
                                                  select bd.IDBillDetail;
                    dr["IDBillDetails"] = "ID-B-De" + querryIDBillDetailCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                    dr["DishName"] = dish.DishName;
                    dr["Price"] = dish.SalePrice;
                    dr["Quantity"] = 1;
                    dr["Promotion"] = null;
                    dr["PaidPrice"] = (int.Parse(dr["Quantity"].ToString()) * float.Parse(dr["Price"].ToString())).ToString();
                    if (IDBill != "Default")
                    {
                        BillDetail billDetail = new BillDetail();
                        billDetail.IDBillDetail = dr["IDBillDetails"].ToString();
                        billDetail.DishName = dish.DishName;
                        billDetail.Price = dish.SalePrice;
                        billDetail.Quantity = int.Parse(dr["Quantity"].ToString());
                        billDetail.Promotion = null;
                        billDetail.PaidPrice = float.Parse(dr["PaidPrice"].ToString());
                        billDetail.IDBill = IDBill;
                        dr["IDBill"] = IDBill;
                        _db.BillDetails.Add(billDetail);

                        _db.SaveChanges();
                    }
                    else
                    {
                        var querryIDBillCount = from b in _db.Bills
                                                select b.IDBill;
                        Bill bill = new Bill();
                        bill.IDBill = "ID-B" + querryIDBillCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                        dr["IDBill"] = bill.IDBill;
                        bill.IDStore = "S0-29112020221332";
                        bill.IDUser = user.IDUser;
                        bill.Time = DateTime.Now;
                        _db.Bills.Add(bill);


                        OrderTrack orderTrack = new OrderTrack();
                        orderTrack.IDBill = bill.IDBill;
                        orderTrack.IDOrderStatse = "OS-01";
                        _db.OrderTracks.Add(orderTrack);

                        BillDetail billDetail = new BillDetail();
                        billDetail.IDBillDetail = dr["IDBillDetails"].ToString();
                        billDetail.DishName = dish.DishName;
                        billDetail.Price = dish.SalePrice;
                        billDetail.Quantity = int.Parse(dr["Quantity"].ToString());
                        billDetail.Promotion = null;
                        billDetail.PaidPrice = float.Parse(dr["PaidPrice"].ToString());
                        billDetail.IDBill = bill.IDBill;
                        _db.BillDetails.Add(billDetail);

                        _db.SaveChanges();
                    }
                    cart.Rows.Add(dr);
                }
                Session["cart"] = cart;
            }
            var success = new
            {
                message = dish.DishName,
                type = true,
            };
            return Json(success, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteCart(int Index)
        {
            if (Session["cart"] != null)
            {
                DataTable dt = new DataTable();
                dt = Session["cart"] as DataTable;
                DataRow dr = dt.Rows[Index];
                string IDBillDetails = dr["IDBillDetails"].ToString();
                BillDetail bd = _db.BillDetails.Find(IDBillDetails);
                if (bd != null)
                {
                    var bdTemp = (from s in _db.BillDetails
                                  where s.IDBill == bd.IDBill
                                  select s).ToList();
                    if (bdTemp.Count > 1)
                    {
                        _db.BillDetails.Remove(bd);
                        _db.SaveChanges();
                        dt.Rows.RemoveAt(Index);
                        Session["cart"] = dt;
                        return Content("true");
                    }
                    else
                    {
                        string IDBill = bd.IDBill;
                        _db.BillDetails.Remove(bd);
                        OrderTrack ot = _db.OrderTracks.Where(n => n.IDBill == IDBill).SingleOrDefault();
                        if (ot != null)
                        {
                            _db.OrderTracks.Remove(ot);
                        }
                        Bill b = _db.Bills.SingleOrDefault(n => n.IDBill == IDBill);
                        if (b != null)
                        {
                            _db.Bills.Remove(b);
                        }
                        _db.SaveChanges();
                        Session.Remove("Cart");
                        return Content("true");
                    }
                }

            }
            return Content("false");
        }

        public ActionResult EditCart(string IDBillDetail, int Quantity)
        {
            User user = Session["User"] as User;
            var role = (from ur in _db.User_Roles
                        where ur.IDUser == user.IDUser
                        select ur).SingleOrDefault();
            if (Session["cart"] != null)
            {
                DataTable dt = new DataTable();
                dt = Session["cart"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["IDBillDetails"].ToString() == IDBillDetail)
                    {
                        if (role.IDRole == "R02")
                        {
                            string temp = dr["DishName"].ToString();
                            Store store = _db.Stores.SingleOrDefault(n => n.IDUser == user.IDUser);
                            if (store != null)
                            {
                                Menu_Stores menu_Stores = (from ms in _db.Menu_Stores
                                                           join m in _db.Menus on ms.IDDish equals m.IDDish
                                                           where m.DishName == temp && ms.IDStore == store.IDStore
                                                           select ms).SingleOrDefault();
                                if (menu_Stores != null)
                                {
                                    if (menu_Stores.Available < Quantity)
                                    {
                                        return Content("Sold-out");
                                    }
                                }
                                else
                                {
                                    return Content("false");
                                }
                            }

                        }
                        dr["Quantity"] = Quantity;
                        dr["PaidPrice"] = (int.Parse(dr["Quantity"].ToString()) * float.Parse(dr["Price"].ToString())).ToString();
                        BillDetail billDetail = _db.BillDetails.SingleOrDefault(n => n.IDBillDetail == IDBillDetail);
                        if (billDetail != null)
                        {
                            billDetail.Quantity = Quantity;
                            billDetail.PaidPrice = float.Parse(dr["PaidPrice"].ToString());
                            _db.BillDetails.AddOrUpdate(billDetail);
                            _db.SaveChanges();
                        }
                        break;
                    }
                }
                Session["cart"] = dt;
                return Content("true");
            }
            return Content("false");
        }
        #endregion
    }
}