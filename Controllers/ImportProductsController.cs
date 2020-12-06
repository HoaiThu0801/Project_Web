using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Web.Models;

namespace Project_Web.Controllers
{
    public class ImportProductsController : Controller
    {
        Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();
        // GET: ImportProducts
        [HttpGet]
        public ActionResult ImportProducts()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ImportProducts(WarehouseDetail model, Warehouse warehouse)
        {
            warehouse = _db.Warehouses.SingleOrDefault(n => n.WarehouseName == warehouse.WarehouseName);
            var querryWarehouseDetailCount = from WarehouseDetail in _db.WarehouseDetails
                                            select WarehouseDetail.IDWarehouseDetail;
            model.IDWarehouseDetail = "WHD" + querryWarehouseDetailCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
            model.IDWarehouse = warehouse.IDWarehouse;
            _db.WarehouseDetails.Add(model);
            _db.SaveChanges();

            return View();
        }

        public JsonResult LoadDishName(string WarehouseName)
        {
            var dish = (from s in _db.Warehouses
                        join a in _db.Stores on s.IDStore equals a.IDStore
                        join b in _db.Menu_Stores on s.IDStore equals b.IDStore
                        join c in _db.Menus on b.IDDish equals c.IDDish
                        where s.WarehouseName == WarehouseName
                        select new
                        {
                            DishName = c.DishName,
                        });
            int dem = dish.Count();
            return Json(dish.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}