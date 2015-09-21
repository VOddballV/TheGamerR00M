using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheGamerR00M.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(Models.UserModel UserInfo)
        {
            ViewBag.Title = "Home Page";
            //  If you're not a user then just return home view
            if (UserInfo == null)
            {
                return View();
            }
            //  Else Set users name
            else
            {
                ViewBag.UserName = UserInfo.UserName;
            }
            return View();
        }

        public ActionResult PageNotFound()
        {
            return View();
        }

        public ActionResult Error()
        {
            return RedirectToRoute("PageNotFound");
        }
    }
}
