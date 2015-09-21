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
            if (UserInfo.UserName == null)
            {
                return View();
            }
            //  Else Set users name
            else
            {
                return View(UserInfo);
            }
        }

        public ActionResult PageNotFound(Models.UserModel UserInfo)
        {
            if (UserInfo.UserName == null)
            {
                return View();
            }
            //  Else Set users name
            else
            {
                return View(UserInfo);
            }
        }

        public ActionResult Error()
        {
            return RedirectToRoute("PageNotFound");
        }
    }
}
