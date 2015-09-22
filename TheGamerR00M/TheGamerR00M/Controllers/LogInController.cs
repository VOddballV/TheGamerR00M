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
        public ActionResult Index(Models.UserModel UserInfo)
        {
            //Check new password with confirmed password
            var UserName = Request.Form["UserName"];
            var Password = Request.Form["Password"];
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
            Session["UserID"] = UserInfo.UserID.ToString();
            Session["UserEmail"] = UserInfo.UserEmail.ToString();
            Session["UserName"] = UserInfo.UserName.ToString();
            Session["UserRankID"] = UserInfo.UserRankID.ToString();
            Session["UserStatusID"] = UserInfo.UserStatusID.ToString();

            //  Find user and validate 
            return RedirectToRoute("Home");
        }

        private UserModel UserDetail(string username)
        {
            UserModel UserInfo = new UserModel();
            using (DB.DB_9D88FA_TheGamerR00MEntities db = new DB.DB_9D88FA_TheGamerR00MEntities())
            {
                //DataSet dsTemp = null;
                var query = db.Users.Where(x=> x.UserName == username).FirstOrDefault();
                //  If user is not found return null
                if (query.UserID == 0)
                {
                    return null;
                }
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