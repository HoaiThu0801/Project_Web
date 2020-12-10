using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Web.Models;

namespace Project_Web.Controllers
{
    public class OrderProductsController : Controller
    {
        Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();
        // GET: OrderProducts
        public ActionResult OrderProducts()
        {
            return View();
        }
        public ActionResult Shipping()
        {
            return View();
        }
        #region LoadData
        [HttpGet]
        public JsonResult LoadDistrict(string ProvinceName)
        {
            Province pro = _db.Provinces.SingleOrDefault(n => n.Name == ProvinceName);
            if (pro != null)
            {
                var districts = (from d in _db.Districts
                                 where d.ProvinceId == pro.Id
                                 select d.Name);
                return Json(districts.ToList(), JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}