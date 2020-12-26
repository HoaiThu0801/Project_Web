using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Web.Models;

namespace Project_Web.Controllers
{
    [Authorize_seller_adminController]
    public class ImportProductsController : BaseController
    {
        Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();
        // GET: ImportProducts        
        #region ImportProducts
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
            Menu menu = _db.Menus.SingleOrDefault(n => n.DishName == model.DishName);
            Menu_Stores menu_store = _db.Menu_Stores.SingleOrDefault(n => n.IDDish == menu.IDDish && n.IDStore == warehouse.IDStore);
            if (menu_store != null)
            {
                menu_store.Available = model.Quantity;
                _db.Menu_Stores.AddOrUpdate(menu_store);
            }
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
                        }).ToList();
            return Json(dish.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}