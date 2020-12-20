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
    public class StatisticController : Controller
    {
        Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();

        // GET: Statistoc
        public ActionResult Statistic()
        {
            return View();
        }
        public ActionResult Statistic_QuantityImportProduct()
        {
            //List<DataPoint_Label_y> dataPoints = new List<DataPoint_Label_y>();

            //var warehouses = (from s in _db.Stores
            //                  join wh in _db.Warehouses on s.IDStore equals wh.IDStore
            //                  select wh).ToList();
            //foreach (var wh in warehouses)
            //{
            //    var warehouseDetails = (from whd in _db.WarehouseDetails
            //                            where whd.IDWarehouse == wh.IDWarehouse
            //                            select whd);
            //    if (warehouseDetails != null)
            //    {
            //        double sum = 0;
            //        foreach (var temp in warehouseDetails)
            //        {
            //            sum = sum + Double.Parse(temp.Quantity.ToString());
            //        }
            //        dataPoints.Add(new DataPoint_Label_y(wh.WarehouseName, sum));
            //    }
            //    else
            //    {
            //        dataPoints.Add(new DataPoint_Label_y(wh.WarehouseName, 0));
            //    }

            //}
            //ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View();
        }
        public ActionResult Statistic_ReveneuProduct()
        {
            //List<DataPoint_Label_y> dataPoint_Label_Y = new List<DataPoint_Label_y>();
            //var store = (from s in _db.Stores
            //             select s).ToList();
            //foreach (var s in store)
            //{
            //    var bill = (from b in _db.Bills
            //                join ot in _db.OrderTracks on b.IDBill equals ot.IDBill
            //                where b.IDStore == s.IDStore && ot.IDOrderStatse == "OS-05"
            //                select b).ToList();
            //    if (bill.Count == 0)
            //    {
            //        dataPoint_Label_Y.Add(new DataPoint_Label_y(s.StoreName, 0));
            //    }
            //    else
            //    {
            //        float sum = 0;
            //        foreach (var temp in bill)
            //        {
            //            sum = sum + float.Parse(temp.Total.ToString());
            //        }
            //        dataPoint_Label_Y.Add(new DataPoint_Label_y(s.StoreName, sum));
            //    }
            //}
            //ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoint_Label_Y);
            return View();
        }
    }
}