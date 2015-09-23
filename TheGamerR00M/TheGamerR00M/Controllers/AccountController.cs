using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGamerR00M.Models;

namespace TheGamerR00M.Controllers
{
    public class AccountController : Controller
    {
        UserModel UserInfo = new UserModel();
        // GET: Account
        public ActionResult Index()
        {
            //  If user is not logged in send to Home
            if (Session.Count == 0)
            {
                return RedirectToRoute("Home");
            }
            else
            {
                //  Set users info from session data
                UserInfo.UserID = Convert.ToInt32(Session["UserID"]);
                UserInfo.UserEmail = Session["UserEmail"].ToString();
                UserInfo.UserName = Session["UserName"].ToString();
                UserInfo.UserRankID = Convert.ToInt32(Session["UserRankID"]);
                UserInfo.UserStatusID = Convert.ToInt32(Session["UserStatusID"]);
                return View(UserInfo);
            }
        }
    }
}