using Project_Web.Models;
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
        public ActionResult Index(int? page)
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
            Session.Remove("cart");
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
        public ActionResult InformationAccount(User model, FormCollection form)
        {
            string Province = form["Province"].ToString();
            string District = form["District"].ToString();
            string Ward = form["Ward"].ToString();
            string Street = form["Street"].ToString();
            User user_session = Session["User"] as User;
            model.Username = user_session.Username;
            Address_Users address_Users = _db.Address_Users.SingleOrDefault(x => x.IDUser == user_session.IDUser && x.IsDefault == 1);
            User user = (from u in _db.Users
                         where u.Username == model.Username
                         select u).SingleOrDefault();
            if (user != null)
            {
                if(address_Users!= null)
                {
                    address_Users.Province = Province;
                    address_Users.District = District;
                    address_Users.Ward = Ward;
                    address_Users.Street = Street;
                    address_Users.Fullname = model.Fullname;
                    address_Users.PhoneNumber = model.PhoneNumber;
                    _db.Address_Users.AddOrUpdate(address_Users);
                    _db.SaveChanges();
                }
                user.Fullname = model.Fullname;
                user.Gender = model.Gender;
                user.DateofBirth = model.DateofBirth;
                user.IdentityCard = model.IdentityCard;
                user.Address = Street;
                user.PhoneNumber = model.PhoneNumber;
                _db.Users.AddOrUpdate(user);
                _db.SaveChanges();
                Session["User"] = user;
                return View();
            }
            return View();
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

        [Authorize_userController]
        [HttpGet]
        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(string Username, string OldPassword, string NewPassword)
        {
            if(OldPassword == NewPassword)
            {
                return Content("IsEquals");
            }    
            EncryptionPW encryptionOld_PW = new EncryptionPW(OldPassword);
            string old_password = encryptionOld_PW.EncryptPass();
            EncryptionPW encryptionNew_PW = new EncryptionPW(NewPassword);
            string new_password = encryptionNew_PW.EncryptPass();
            User user = _db.Users.SingleOrDefault(x => x.Username == Username && x.Password == old_password);
            if(user != null)
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
        [Authorize_userController]
        public ActionResult ShoppingCart()
        {
            return View();
        }
        #region Cart
        [HttpPost]
        [Authorize_userController]
        public ActionResult AddCart(string IDDish)
        {
            User user = Session["User"] as User;
            if (user == null)
            {
                return RedirectToAction("SignIn", "SignIn");
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
            return Content("true");
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