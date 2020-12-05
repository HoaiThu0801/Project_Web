using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Web.Models
{
    public class Authorize_userController: ActionFilterAttribute
    {
        Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //User tuser = HttpContext.Current.Session["user"] as User;
            int iUserID = Convert.ToInt32(HttpContext.Current.Session["Is Login"]);
            if (iUserID != 1)
            {
                filterContext.Result = new RedirectResult("~/SignIn/SignIn");
            }
        }
    }
}