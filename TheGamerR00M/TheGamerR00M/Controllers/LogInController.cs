using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGamerR00M.Models;

namespace TheGamerR00M.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Models.UserModel UserInfo)
        {
            //Check new password with confirmed password
            var UserName = Request.Form["UserName"];
            var Password = Request.Form["Password"];
            //  Find user and validate 
            UserInfo = UserDetail(UserName);
            // Is that userName valid
            if (UserInfo == null)
            {
                TempData = null;
                TempData.Add("errorInvalid", "User doesn't exist");
                return View("Index");
            }
            // Does password match with users password
            if (UserInfo.UserPass != Password)
            {
                TempData = null;
                TempData.Add("errorInvalid", "Invalid Password");
                return View("Index");               
            }
            return RedirectToRoute("Home", UserInfo);
        }

        private UserModel UserDetail(string username)
        {
            UserModel UserInfo = new UserModel();
            using (DB.DB_9D88FA_TheGamerR00MEntities db = new DB.DB_9D88FA_TheGamerR00MEntities){
                //DataSet dsTemp = null;
                var query = db.Users.Where(x=> x.UserName == username).FirstOrDefault();
                //  Assign Values to UserInfo
                UserInfo.UserEmail = query.UserEmail;
                UserInfo.UserID = query.UserID;
                UserInfo.UserName = query.UserName;
                UserInfo.UserPass = query.UserPass;
                UserInfo.UserRankID = query.UserRankID;
                UserInfo.UserStatusID = query.UserStatusID;
            }
            return UserInfo;
        }
    }
}