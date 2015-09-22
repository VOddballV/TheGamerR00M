using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        [MultipleButton(Name = "action", Argument = "LogIn")]
        public ActionResult LogIn(Models.UserModel UserInfo) 
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

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Register")]
        public ActionResult Register(Models.UserModel UserInfo)
        {
            var UserName = Request.Form["UserName"];
            var Password = Request.Form["Password"];
            var ConfirmPass = Request.Form["ConfirmPassword"];
            var UserEmail = Request.Form["UserEmail"];
            
            //  Get list of UserNames
            List<UserModel> users = lstUsers();

            foreach (var item in users)
            {
                if (item.UserName == UserName)
                {
                    TempData = null;
                    TempData.Add("errorInvalid", "Username already in use");
                    return View("Index");
                }
            }

            if (Password != ConfirmPass)
            {
                TempData = null;
                TempData.Add("errorInvalid", "Passwords do not match");
                return View("Index");
            }

            //  Create the user
            CreateUser(UserName, Password, UserEmail);

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

        private List<UserModel> lstUsers()
        {
            List<UserModel> UserInfo = new List<UserModel>();
            using (DB.DB_9D88FA_TheGamerR00MEntities db = new DB.DB_9D88FA_TheGamerR00MEntities())
            {
                //DataSet dsTemp = null;
                var query = db.Users.Select(x => x.UserName);

                foreach(var user in query){
                        UserModel item = new UserModel();
                        item.UserName = user;
                        UserInfo.Add(item);
                }
            }
            return UserInfo;
        }

        private void CreateUser(string username, string password, string email)
        {   
            DB.User UserInfo = new DB.User();
            using (DB.DB_9D88FA_TheGamerR00MEntities db = new DB.DB_9D88FA_TheGamerR00MEntities())
            {
                
                //  Get count of users to set userid
                int userCount = db.Users.Select(x => x.UserName).Count();
                //  Set Users info
                UserInfo.UserName = username;
                UserInfo.UserPass = password;
                UserInfo.UserEmail = email;
                UserInfo.UserRankID = 3;
                UserInfo.UserStatusID = 1;
                UserInfo.UserID = userCount + 1;
                //  Insert into Users table
                db.Users.Add(UserInfo);
                //  Save user info in DB
                db.SaveChanges();
            }
            //  log user in and create a session
            Session["UserID"] = UserInfo.UserID.ToString();
            Session["UserEmail"] = UserInfo.UserEmail.ToString();
            Session["UserName"] = UserInfo.UserName.ToString();
            Session["UserRankID"] = UserInfo.UserRankID.ToString();
            Session["UserStatusID"] = UserInfo.UserStatusID.ToString();
        }

        //
        //  Handles multiple submit buttons
        //
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public class MultipleButtonAttribute : ActionNameSelectorAttribute
        {
            public string Name { get; set; }
            public string Argument { get; set; }

            public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
            {
                var isValidName = false;
                var keyValue = string.Format("{0}:{1}", Name, Argument);
                var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

                if (value != null)
                {
                    controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
                    isValidName = true;
                }

                return isValidName;
            }
        }
    }
}