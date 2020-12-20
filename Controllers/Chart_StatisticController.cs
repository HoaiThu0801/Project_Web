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
    public class Chart_StatisticController : Controller
    {
        Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();
        // GET: Chart_Statistic
        public ActionResult Chart_Statistic()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
 
			dataPoints.Add(new DataPoint("Economics", 1));
			dataPoints.Add(new DataPoint("Physics", 2));
			dataPoints.Add(new DataPoint("Literature", 4));
			dataPoints.Add(new DataPoint("Chemistry", 4));
			dataPoints.Add(new DataPoint("Literature", 9));
			dataPoints.Add(new DataPoint("Physiology or Medicine", 11));
			dataPoints.Add(new DataPoint("Peace", 13));
			ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View();
        }
        //[HttpGet]
        //public JsonResult LoadQuantityImportProduct()
        //{
        //    List<DataPoint> dataPoints = new List<DataPoint>();
        //    var warehouses = (from s in _db.Stores
        //                 join wh in _db.Warehouses on s.IDStore equals wh.IDStore
        //                 select wh).ToList();
        //    foreach (var wh in warehouses)
        //    {
        //        var warehouseDetails = (from whd in _db.WarehouseDetails
        //                           where whd.IDWarehouse == wh.IDWarehouse
        //                           select whd);
        //        if (warehouseDetails != null)
        //        {
        //            double sum = 0;
        //            foreach (var temp in warehouseDetails)
        //            {
        //                sum = sum + Double.Parse(temp.Quantity.ToString());
        //            }
        //            dataPoints.Add(new DataPoint(wh.WarehouseName,sum));
        //        }
        //        else
        //        {
        //            dataPoints.Add(new DataPoint(wh.WarehouseName, 0));
        //        }
                
        //    }
        //    return Json(dataPoints, JsonRequestBehavior.AllowGet);
        //}

    }
}