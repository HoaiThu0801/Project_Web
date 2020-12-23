using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Web.Controllers
{
    public class BaseController : Controller
    {
        protected void SetAlert(string title, string message, bool type)
        {
            TempData["AlertTitle"] = title;
            TempData["AlertMessage"] = message;
            if (type == true)
            {
                TempData["AlertType"] = "alert-success";
                TempData["AlertIcon"] = "fa fa-check-circle";
            }
            else if (type == false) 
            {
                TempData["AlertType"] = "alert-danger";
                TempData["AlertIcon"] = "fas fa-exclamation-circle";
            }
        }
    }
}