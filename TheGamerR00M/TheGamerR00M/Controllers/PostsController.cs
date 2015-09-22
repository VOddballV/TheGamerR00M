using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGamerR00M.Models;

namespace TheGamerR00M.Controllers
{
    public class PostsController : Controller
    {
        UserModel UserInfo = new UserModel();

        public ActionResult Reviews()
        {
            UserInfo.UserID = Convert.ToInt32(Session["UserID"]);
            if (UserInfo.UserID == 0)
            {
                return View();
            }
            else
            {
                getUserDetails(UserInfo.UserID);
                return View();
            }
        }

        public ActionResult Stories()
        {
            UserInfo.UserID = Convert.ToInt32(Session["UserID"]);
            if (UserInfo.UserID == 0)
            {
                return View();
            }
            else
            {
                getUserDetails(UserInfo.UserID);
                return View();
            }
        }

        private UserModel getUserDetails(int userID)
        {
            using (DB.DB_9D88FA_TheGamerR00MEntities db = new DB.DB_9D88FA_TheGamerR00MEntities())
            {
                //DataSet dsTemp = null;
                var query = db.Users.Where(x => x.UserID == userID).FirstOrDefault();
                //  If user is not found return null
                if (query.UserID == 0)
                {
                    return null;
                }
                //  Assign Values to UserInfo
                UserInfo.UserEmail = query.UserEmail;
                UserInfo.UserID = query.UserID;
                UserInfo.UserName = query.UserName;
                UserInfo.UserRankID = query.UserRankID;
                UserInfo.UserStatusID = query.UserStatusID;
                //  Update session data
                Session["UserID"] = query.UserID.ToString();
                Session["UserEmail"] = query.UserEmail.ToString();
                Session["UserName"] = query.UserName.ToString();
                Session["UserRankID"] = query.UserRankID.ToString();
                Session["UserStatusID"] = query.UserStatusID.ToString();

            }
            return UserInfo;
        }
    }
}