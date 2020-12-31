using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Web.Models;
using PagedList;
using System.Data.Entity.Migrations;
using ClosedXML.Excel;
using System.Data;
using System.IO;

namespace Project_Web.Controllers
{
    public class OrderManagementController : BaseController
    {
        Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();
        // GET: OrderManagement
        public ActionResult OrderManagement()
        {
            return View();
        }

        #region MovingState
        public ActionResult StatePreparing(string IDBill)
        {
            OrderTrack orderTrack = _db.OrderTracks.SingleOrDefault(n => n.IDBill == IDBill);
            if (orderTrack != null)
            {
                OrderTrack ot = new OrderTrack();
                ot.IDBill = orderTrack.IDBill;
                ot.IDOrderStatse = "OS-03";
                _db.OrderTracks.Remove(orderTrack);
                _db.OrderTracks.Add(ot);
                _db.SaveChanges();
            }
            return RedirectToAction("OrderManagement", "OrderManagement");
        }
        public ActionResult StateDelivering(string IDBill)
        {
            OrderTrack orderTrack = _db.OrderTracks.SingleOrDefault(n => n.IDBill == IDBill);
            if (orderTrack != null)
            {
                OrderTrack ot = new OrderTrack();
                ot.IDBill = orderTrack.IDBill;
                ot.IDOrderStatse = "OS-04";
                _db.OrderTracks.Remove(orderTrack);
                _db.OrderTracks.Add(ot);
                _db.SaveChanges();
            }
            return RedirectToAction("OrderManagement", "OrderManagement");
        }
        public ActionResult StatePaid(string IDBill)
        {
            OrderTrack orderTrack = _db.OrderTracks.SingleOrDefault(n => n.IDBill == IDBill);
            if (orderTrack != null)
            {
                OrderTrack ot = new OrderTrack();
                ot.IDBill = orderTrack.IDBill;
                ot.IDOrderStatse = "OS-05";
                _db.OrderTracks.Remove(orderTrack);
                _db.OrderTracks.Add(ot);
                _db.SaveChanges();
            }
            return RedirectToAction("OrderManagement", "OrderManagement");
        }
        #endregion

        #region UserManagement
        public ActionResult UserManagement()
        {
            return View();
        }
        public ActionResult DeleteUser (string IDUser)
        {
            var user = _db.Users.SingleOrDefault(n => n.IDUser == IDUser);
            var role = (from ur in _db.User_Roles
                        where ur.IDUser == IDUser
                        select ur).SingleOrDefault();
            if (role.IDRole == "R02")
            {
                Store store = _db.Stores.SingleOrDefault(n => n.IDUser == IDUser);
                if (store != null)
                {
                    store.IDUser = null;
                    _db.Stores.AddOrUpdate(store);
                    _db.SaveChanges();
                }
            }
            List<Bill> bills = (from b in _db.Bills
                         where b.IDUser == IDUser
                         select b).ToList();
            foreach (Bill b in bills)
            {
                List<BillDetail> billDetails = (from bd in _db.BillDetails
                                                where bd.IDBill == b.IDBill
                                                select bd).ToList();
                foreach (BillDetail bd in billDetails)
                {
                    _db.BillDetails.Remove(bd);
                }
                _db.SaveChanges();
                OrderTrack orderTrack = _db.OrderTracks.SingleOrDefault(n => n.IDBill == b.IDBill);
                if (orderTrack != null)
                {
                    _db.OrderTracks.Remove(orderTrack);
                }
                _db.SaveChanges();
                _db.Bills.Remove(b);
                _db.SaveChanges();
            }
            List<Address_Users> address_Users = (from au in _db.Address_Users
                                                 where au.IDUser == IDUser
                                                 select au).ToList();
            foreach (Address_Users au in address_Users)
            {
                _db.Address_Users.Remove(au);
            }
            _db.SaveChanges();
            _db.User_Roles.Remove(role);
            _db.SaveChanges();
            _db.Users.Remove(user);
            _db.SaveChanges();
            var success = new
            {
                message = user.Username,
                type = true,            
            };
            return Json(success, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region InformationProducts
        public ActionResult InformationProducts()
        {
            return View();
        }
        #endregion

        #region Search
        public ActionResult JsonSearch_OrderManagement(string DataSearch)
        {
            List<Bill> billList = new List<Bill>();
            var billDetails = _db.BillDetails.Where(n => n.DishName.Contains(DataSearch));
            foreach (var bd in billDetails)
            {
                var billTemp = billList.SingleOrDefault(n => n.IDBill == bd.IDBill);
                if (billTemp == null)
                {
                    Bill bill = _db.Bills.SingleOrDefault(n => n.IDBill == bd.IDBill);
                    if (bill != null)
                    {
                        billList.Add(bill);
                    }
                }
            }
            Session["InputSearch"] = DataSearch;
            Session["Search"] = billList;
            return Content("True");
        }
        #endregion

        #region RevenueManagement
        public ActionResult RevenueManagement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TrackRevenue(DateTime StartTime, DateTime EndTime)
        {
            if (Session["User"] != null)
            {
                User user = Session["User"] as User;
                var role = _db.User_Roles.SingleOrDefault(n => n.IDUser == user.IDUser);
                if (role.IDRole == "R02")
                {
                    var store = _db.Stores.SingleOrDefault(n => n.IDUser == user.IDUser);
                    if (store != null)
                    {
                        List<RevenueofStore> revenueofStores = new List<RevenueofStore>();
                        var Bills = (from b in _db.Bills
                                     join ot in _db.OrderTracks on b.IDBill equals ot.IDBill
                                     where b.IDStore == store.IDStore && (StartTime <= b.Time && EndTime >= b.Time) && ot.IDOrderStatse == "OS-05"
                                     select b).ToList();
                        foreach (var b in Bills)
                        {
                            RevenueofStore revenueofStoreTemp = new RevenueofStore();
                            revenueofStoreTemp.StoreName = store.StoreName;
                            revenueofStoreTemp.Address = store.Location;
                            revenueofStoreTemp.FullName = user.Fullname;
                            revenueofStoreTemp.Revenue = float.Parse(b.Total.ToString());
                            revenueofStoreTemp.Time = DateTime.Parse(b.Time.ToString());
                            revenueofStores.Add(revenueofStoreTemp);
                        }
                        return Json(revenueofStores, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Content("False");
                    }
                }
                else if (role.IDRole == "R01")
                {
                    List<RevenueofStore> revenueofStores = new List<RevenueofStore>();
                    var Bills = (from b in _db.Bills
                                 join ot in _db.OrderTracks on b.IDBill equals ot.IDBill
                                 where (StartTime <= b.Time && EndTime >= b.Time) && ot.IDOrderStatse == "OS-05"
                                 select b).ToList();
                    foreach (var b in Bills)
                    {
                        var store = _db.Stores.SingleOrDefault(n => n.IDStore == b.IDStore);
                        RevenueofStore revenueofStoreTemp = new RevenueofStore();
                        revenueofStoreTemp.StoreName = store.StoreName;
                        revenueofStoreTemp.Address = store.Location;
                        revenueofStoreTemp.FullName = user.Fullname;
                        revenueofStoreTemp.Revenue = float.Parse(b.Total.ToString());
                        revenueofStoreTemp.Time = DateTime.Parse(b.Time.ToString());
                        revenueofStores.Add(revenueofStoreTemp);
                    }
                    return Json(revenueofStores, JsonRequestBehavior.AllowGet);
                }
                return RedirectToAction("SignIn", "SignIn");
               
            }
            return RedirectToAction("SignIn", "SignIn");
        }
        #endregion
        public FileResult ExportTrack(DateTime StartTime, DateTime EndTime)
        {
            if (Session["User"] != null)
            {
                User user = Session["User"] as User;
                var role = _db.User_Roles.SingleOrDefault(n => n.IDUser == user.IDUser);
                if (role.IDRole == "R02")
                {
                    var store = _db.Stores.SingleOrDefault(n => n.IDUser == user.IDUser);
                    if (store != null)
                    {
                        DataTable dt = new DataTable("Grid");
                        dt.Columns.AddRange(new DataColumn[5] { new DataColumn("StoreName"),
                                            new DataColumn("Location"),
                                            new DataColumn("Fullname"),
                                            new DataColumn("Revenue"),
                                            new DataColumn("Time")});
                        var Bills = (from b in _db.Bills
                                     join ot in _db.OrderTracks on b.IDBill equals ot.IDBill
                                     where b.IDStore == store.IDStore && (StartTime <= b.Time && EndTime >= b.Time) && ot.IDOrderStatse == "OS-05"
                                     select b).ToList();
                        foreach (var b in Bills)
                        {
                            dt.Rows.Add(store.StoreName, store.Location, user.Fullname, float.Parse(b.Total.ToString()), DateTime.Parse(b.Time.ToString()));
                        }
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {
                                wb.SaveAs(stream);
                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                            }
                        }
                    }
                }
            }
            string path = "~/Templates/File_20201221_v1.0_TemplateImportProduct.xlsx";
            return File(path, "application/vnd.ms-excel", "File_20201221_v1.0_TemplateImportProduct.xlsx");
        }

        public ActionResult DishesManagement()
        {
            return View();
        }
        public ActionResult StoresManagement()
        {
            return View();
        }
        public ActionResult ImportProductsManagement()
        {
            return View();
        }
    }
}