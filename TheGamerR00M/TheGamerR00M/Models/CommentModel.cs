using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheGamerR00M.Models
{
    public class CommentModel
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public string CommentBody { get; set; }
        public int Comment_CUserID { get; set; }
        public DateTime Comment_CDate { get; set; }
        public string CommentAuthor { get; set; }
    }
}