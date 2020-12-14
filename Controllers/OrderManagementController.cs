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
        public ActionResult OrderManagement()
        {
            return View();
        }
    }
}