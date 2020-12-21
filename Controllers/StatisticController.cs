using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Web.Models;
using Newtonsoft.Json;
using System.Dynamic;

namespace Project_Web.Controllers
{
    [Authorize_seller_adminController]
    public class StatisticController : Controller
    {
        Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();

        // GET: Statistoc
        public ActionResult Statistic()
        {
            return View();
        }
    }
}