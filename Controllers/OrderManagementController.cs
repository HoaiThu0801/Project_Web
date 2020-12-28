using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Web.Models;
using PagedList;

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
    }
}