using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Web.Models;
using PagedList;

namespace Project_Web.Controllers
{
    
    public class OrderManagementController : Controller
    {
        Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();
        // GET: OrderManagement
        public ActionResult OrderManagement(int? page)
        {
            if (page == null) page = 1;
            var billDetails = (from bd in _db.Bills
                        select bd).OrderBy(x => x.IDBill);
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(billDetails.ToPagedList(pageNumber, pageSize));
        }
    }
}