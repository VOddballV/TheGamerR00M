using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheGamerR00M.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index(Models.UserModel UserInfo)
        {
            return View(UserInfo);
        }
    }
}