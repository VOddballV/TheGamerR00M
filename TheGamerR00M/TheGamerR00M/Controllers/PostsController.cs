using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TheGamerR00M.Models;

namespace TheGamerR00M.Controllers
{
    public class PostsController : Controller
    {
        UserModel UserInfo = new UserModel();
        PostModel PostInfo = new PostModel();
        List<CommentModel> CommentList = new List<CommentModel>();
        List<PostModel> PostList = new List<PostModel>();

        public ActionResult Reviews()
        {
            //  Get list of Posts
            List<PostModel> posts = getPostList(1);
            // Get the users ID from session data
            UserInfo.UserID = Convert.ToInt32(Session["UserID"]);
            // If user id is null return normal view
            if (UserInfo.UserID == 0)
            {
                return View(posts);
            }
            //  Else Return view of poster
            else
            {
                //  Get users updated information and set model
                getUserDetails(UserInfo.UserID);
                return View(posts);
            }
        }

        public ActionResult Stories()
        {
            //  Get list of Posts
            List<PostModel> posts = getPostList(2);
            // Get the users ID from session data
            UserInfo.UserID = Convert.ToInt32(Session["UserID"]);
            // If user id is null return normal view
            if (UserInfo.UserID == 0)
            {
                return View(posts);
            }
            //  Else Return view of poster
            else
            {
                //  Get users updated information and set model
                getUserDetails(UserInfo.UserID);
                return View(posts);
            }
        }

        public ActionResult gotoPost(int postID = 0)
        {
            if (postID == 0)
            {
                return RedirectToRoute("Home");
            }
            //  Get list of Posts
            PostModel post = getPost(postID);
            //  If post ID = 0 then post doesn't exist
            if (post.PostID == 0)
            {
                return RedirectToRoute("PageNotFound");
            }
            // Get the users ID from session data
            UserInfo.UserID = Convert.ToInt32(Session["UserID"]);
            //  Get comments 
            getCommentList(postID);
            var viewModel = new PostViewModel();
            viewModel.CommentList = CommentList;
            viewModel.PostInfo = post;
            // If user id is null return normal view
            if (UserInfo.UserID == 0)
            {
                return View(viewModel);
            }
            //  Else Return view of poster
            else
            {
                //  Get users updated information and set model
                getUserDetails(UserInfo.UserID);
                return View(viewModel);
            }
        }

        public ActionResult NewPost()
        {
            // Get the users ID from session data
            UserInfo.UserID = Convert.ToInt32(Session["UserID"]);
            UserInfo.UserRankID = Convert.ToInt32(Session["UserRankID"]);
            // If user id is null return normal view
            // Or if User doesn't have the rights
            if (UserInfo.UserID == 0 || UserInfo.UserRankID == 3)
            {
                return RedirectToRoute("Home");
            }
            //  Else Return view of poster
            else
            {
                //  Get users updated information and set model
                getUserDetails(UserInfo.UserID);
                return View(UserInfo);
            }
        }

        [HttpPost]
        public ActionResult PostComment (PostModel combinedView)
        {
            //  Get form data
            var CommentBody = combinedView.PostBody;
            //  Replace newline with <br/><br/>
            CommentBody = CommentBody.Replace(System.Environment.NewLine, "<br />");
            //  Set temp comment object
            DB.UserComment tempComment = new DB.UserComment();
            using (DB.DB_9D88FA_TheGamerR00MEntities db = new DB.DB_9D88FA_TheGamerR00MEntities())
            {
                var commentCount = db.UserComments.Count();

                //  Set Post info
                tempComment.CommentBody = CommentBody;
                tempComment.CommentID = commentCount + 1;
                tempComment.PostID = combinedView.PostID;
                tempComment.Comment_CUserID = Convert.ToInt32(Session["UserID"]);
                tempComment.Comment_CDate = DateTime.Now;
                db.UserComments.Add(tempComment);
                //  Save tempComment in DB
                db.SaveChanges();
            }
            getCommentList(combinedView.PostID);
            //return new EmptyResult();
            return Json(CommentList);
        }

        [HttpPost]
        public ActionResult SaveEditPost(PostModel postInfo)
        {
            //  Get form data
            var PostType = Request.Form["PostType"];
            var PostTitle = Request.Form["PostTitle"];
            var PostBody = Request.Form["PostBodyEdit"];
            var PostTag = Request.Form["PostTag"];
            int PostTypeID = postInfo.PostTypeID;
            //  Replace newline with <br/><br/>
            PostBody = PostBody.Replace(System.Environment.NewLine, "<br />");
            //  Set PostTypeID based on Post type
            if (PostTypeID == 1)
            {
                PostType = "Reviews";
            }
            else if (PostTypeID == 2)
            {
                PostType = "Stories";
            }
            //  Set temp post object
            using (DB.DB_9D88FA_TheGamerR00MEntities db = new DB.DB_9D88FA_TheGamerR00MEntities())
            {
                var tempPost = db.UsersPosts.Find(postInfo.PostID);
                //  Set Post info
                tempPost.PostBody = PostBody;
                //  Save user info in DB
                db.SaveChanges();
            }
            return RedirectToRoute(PostType);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SavePost(UserModel userInfo, HttpPostedFileBase file)
        {
            //  Get form data
            var PostType = Request.Form["PostType"];
            var PostTitle = Request.Form["PostTitle"];
            var PostBody = Request.Form["PostBody"];
            var PostTag = Request.Form["PostTag"];
            string imageLoadPath = "http://www.gamerr00m.org/ap/";
            int PostTypeID = 0;
            //  Replace newline with <br/><br/>
            PostBody = PostBody.Replace(System.Environment.NewLine, "<br />");
            //  Set PostTypeID based on Post type
            if (PostType == "Reviews")
            {
                PostTypeID = 1;
            }
            else if (PostType == "Stories")
            {
                PostTypeID = 2;
            }
            //  Set file properties and save to server
            var siteRoot = AppDomain.CurrentDomain.BaseDirectory;
            string ImageSaveFilePath = siteRoot + "ap\\",
                    myUniqueFileName = string.Format(@"{0}.jpeg", Guid.NewGuid()),
                    fileName = myUniqueFileName;
            imageLoadPath += fileName;
            ImageSaveFilePath += fileName;
            file.SaveAs(ImageSaveFilePath);
            //  Set temp post object
            DB.UsersPost tempPost = new DB.UsersPost();
            using (DB.DB_9D88FA_TheGamerR00MEntities db = new DB.DB_9D88FA_TheGamerR00MEntities())
            {
                int postCount = db.UsersPosts.Count();
                //  Set Post info
                tempPost.Post_CDate = DateTime.Now;
                tempPost.Post_CUserID = userInfo.UserID;
                tempPost.PostBody = PostBody;
                tempPost.PostID = postCount + 1;
                tempPost.PostImageURL = imageLoadPath;
                tempPost.PostTags = PostTag;
                tempPost.PostTitle = PostTitle;
                tempPost.PostTypeID = PostTypeID;
                db.UsersPosts.Add(tempPost);
                //  Save user info in DB
                db.SaveChanges();
            }
            return RedirectToRoute(PostType);
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

        private List<PostModel> getPostList(int typeID)
        {
            using (DB.DB_9D88FA_TheGamerR00MEntities db = new DB.DB_9D88FA_TheGamerR00MEntities())
            {
                var query = db.UsersPosts.Where(x => x.PostTypeID == typeID).OrderByDescending(x => x.Post_CDate);
                //  Assign Values to PostInfo
                foreach (var post in query)
                {
                    PostModel item = new PostModel();
                    item.Post_CDate = post.Post_CDate;
                    item.Post_CUserID = post.Post_CUserID;
                    item.PostBody = post.PostBody;
                    item.PostID = post.PostID;
                    item.PostImageURL = post.PostImageURL;
                    item.PostTags = post.PostTags;
                    item.PostTitle = post.PostTitle;
                    item.Post_Author = post.User.UserName;
                    //  Add post to postlist
                    PostList.Add(item);
                }
            }
            return PostList;
        }

        private List<CommentModel> getCommentList(int postID)
        {
            using (DB.DB_9D88FA_TheGamerR00MEntities db = new DB.DB_9D88FA_TheGamerR00MEntities())
            {
                //  Get list of comments
                var query = db.UserComments.Where(x => x.PostID == postID).OrderByDescending(x => x.Comment_CDate);
                //  Assign Values to PostInfo
                foreach (var comment in query)
                {
                    CommentModel item = new CommentModel();
                    item.Comment_CDate = comment.Comment_CDate;
                    item.Comment_CUserID = comment.Comment_CUserID;
                    item.CommentBody = comment.CommentBody;
                    item.CommentID = comment.CommentID;
                    item.CommentAuthor = comment.User.UserName;
                    //  Add post to postlist
                    CommentList.Add(item);
                }
            }
            return CommentList;
        }

        private PostModel getPost(int postID)
        {
            using (DB.DB_9D88FA_TheGamerR00MEntities db = new DB.DB_9D88FA_TheGamerR00MEntities())
            {
                //  Get the post by postID
                var query = db.UsersPosts.Where(x => x.PostID == postID).FirstOrDefault();
                if (query == null)
                {
                    return PostInfo;
                }
                //  Set Post details
                PostInfo.Post_CDate = query.Post_CDate;
                PostInfo.Post_Author = query.User.UserName;
                PostInfo.PostBody = query.PostBody;
                PostInfo.PostTypeID = query.PostTypeID;
                PostInfo.PostID = query.PostID;
                PostInfo.PostImageURL = query.PostImageURL;
                PostInfo.PostTags = query.PostTags;
                PostInfo.PostTitle = query.PostTitle;
            }
            return PostInfo;
        }
    }
}