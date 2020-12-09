using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Web.Controllers
{
    public class OrderProductsController : Controller
    {
        // GET: OrderProducts
        public ActionResult OrderProducts()
        {
            return View();
        }
        public ActionResult Shipping()
        {
            return View();
        }
    }
}