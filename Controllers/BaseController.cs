using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Web.Controllers
{
    public class BaseController : Controller
    {
        protected void SetAlert(string title, string message, string type)
        {
            TempData["AlertTitle"] = title;
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
                TempData["AlertIcon"] = "fa fa-check-circle";
            }
            else if (type == "error") 
            {
                TempData["AlertType"] = "alert-danger";
                TempData["AlertIcon"] = "fas fa-exclamation-circle";
            }
        }
    }
}