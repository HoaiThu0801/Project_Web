using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Web.Models;
using PagedList;

namespace Project_Web.Controllers
{
    [Authorize_seller_adminController]
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

    }
}