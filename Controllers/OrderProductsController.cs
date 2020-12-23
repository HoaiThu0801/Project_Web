using MailKit.Net.Smtp;
using MimeKit;
using MailKit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Web.Models;

namespace Project_Web.Controllers
{
    public class OrderProductsController : BaseController
    {
        Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();
        // GET: OrderProducts
        [HttpGet]
        [Authorize_userController]
        public ActionResult OrderProducts()
        {
            return View();
        }
        [HttpPost]
        [Authorize_userController]
        public ActionResult OrderProducts(string AddressOrder)
        {
            User user = new User();
            user = Session["User"] as User;
            DataTable cart = new DataTable();
            cart = Session["cart"] as DataTable;
            var role = (from ur in _db.User_Roles
                       where ur.IDUser == user.IDUser
                       select ur.IDRole).SingleOrDefault();
            DataRow dr = cart.Rows[0];
            string IDBill = dr["IDBill"].ToString();
            OrderTrack orderTrack = _db.OrderTracks.SingleOrDefault(n => n.IDBill == IDBill);
            if (orderTrack != null)
            {
                if (role == "R02")
                {
                    var IDStore = (from s in _db.Stores
                                   where s.IDUser == user.IDUser
                                   select s.IDStore).SingleOrDefault();
                    Bill bill = _db.Bills.SingleOrDefault(n => n.IDBill == IDBill);
                    if (bill != null)
                    {
                        bill.IDStore = IDStore;
                        _db.Bills.AddOrUpdate(bill);
                        _db.SaveChanges();
                    }
                    else
                    {
                        return Content("false");
                    }
                    float sum = 0;
                    foreach(DataRow dataRow in cart.Rows)
                    {
                        sum = sum + (float.Parse(dataRow["PaidPrice"].ToString()));
                        string Dishname = dataRow["DishName"].ToString();
                        Menu dish = _db.Menus.SingleOrDefault(n => n.DishName == Dishname);
                        Menu_Stores menu_Stores = _db.Menu_Stores.SingleOrDefault(n => n.IDStore == IDStore && dish.IDDish == n.IDDish);
                        if (menu_Stores != null)
                        {
                            menu_Stores.Available = menu_Stores.Available - (int.Parse(dataRow["Quantity"].ToString()));
                            _db.Menu_Stores.AddOrUpdate();
                            _db.SaveChanges();
                        }
                    }
                    bill.Total = sum + 22000;
                    _db.Bills.AddOrUpdate(bill);
                    OrderTrack orderTrackTemp = new OrderTrack();
                    orderTrackTemp.IDBill = orderTrack.IDBill;
                    orderTrackTemp.IDOrderStatse = "OS-04";
                    _db.OrderTracks.Remove(orderTrack);
                    _db.OrderTracks.Add(orderTrackTemp);
                    _db.SaveChanges();
                    //SendMail(AddressOrder);
                    Session.Remove("cart");
                    return Content("true");
                }
                else
                {

                    Bill bill = _db.Bills.SingleOrDefault(n => n.IDBill == IDBill);
                    if (bill != null)
                    {
                        float sum = 0;
                       foreach(DataRow dataRow in cart.Rows)
                       {
                            sum = sum + (float.Parse(dataRow["PaidPrice"].ToString()));
                       }
                        bill.Total = sum + 22000;
                        if (user.Point != null)
                        {
                            user.Point = user.Point + bill.Total / 20000;

                        }
                        else
                        {
                            user.Point = bill.Total / 20000;
                        }
                        _db.Bills.AddOrUpdate(bill);
                    }
                    else
                    {
                        return Content("false");
                    }
                    OrderTrack orderTrackTemp = new OrderTrack();
                    orderTrackTemp.IDBill = orderTrack.IDBill;
                    orderTrackTemp.IDOrderStatse = "OS-02";
                    _db.OrderTracks.Remove(orderTrack);
                    _db.OrderTracks.Add(orderTrackTemp);
                    _db.SaveChanges();
                    //SendMail(AddressOrder);
                    Session.Remove("cart");
                    return Content("true");
                }
            }
            else
            {
                return Content("false");
            }
        }

        public void SendMail (string address)
        {
            User user = new User();
            user = Session["User"] as User;
            if (user.Email != null)
            {

                var message = new MimeMessage();
                //From Address
                message.From.Add(new MailboxAddress("Huu Tuong", "huutuong1403@gmail.com"));
                //To Address
                message.To.Add(new MailboxAddress("Dot net", user.Email));
                //Subject
                message.Subject = "Xác nhận đơn hàng -  OrderConfirmation";
                //Body
                message.Body = new TextPart
                {
                    Text = "Địa chỉ giao hàng của bạn: " + address
                };
                //Configure send email
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("huutuong1403@gmail.com", "14032018");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
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
            Address_Users address_UsersTemp = _db.Address_Users.SingleOrDefault(n => n.IDUser == user.IDUser && n.IsDefault == 1);
            Address_Users address_Users = new Address_Users();
            if (address_UsersTemp != null)
            {
                address_Users.IsDefault = 0;
            }
            else
            {
                address_Users.IsDefault = 1;
            }
            address_Users.IDUser = user.IDUser;
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
        [HttpPost]
        public ActionResult DefaultShipping(string IDAddress, string IDUser)
        {
            Address_Users address_Users = _db.Address_Users.SingleOrDefault(n => n.IDUser == IDUser && n.IsDefault == 1);
            if (address_Users != null)
            {
                address_Users.IsDefault = 0;
                _db.Address_Users.AddOrUpdate(address_Users);
                _db.SaveChanges();
            }
            Address_Users address_Users_temp = _db.Address_Users.SingleOrDefault(n => n.IDAddress == IDAddress);
            if (address_Users_temp != null)
            {
                address_Users_temp.IsDefault = 1;
                _db.Address_Users.AddOrUpdate(address_Users_temp);
                _db.SaveChanges();
                return Content("true");
            }
            return Content("false");
        }

        [HttpPost]
        public ActionResult EditAddressShipping(string IDAddress)
        {
            Address_Users address_Users = _db.Address_Users.SingleOrDefault(n => n.IDAddress == IDAddress);
            if (address_Users != null)
            {
                string address = address_Users.Street + " " + address_Users.Ward + " " + address_Users.District + " " +  address_Users.Province;
                Session["AddressOrder"] = address;
                return Content(address);
            }
            return Content("false");
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