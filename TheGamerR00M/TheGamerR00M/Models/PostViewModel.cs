using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheGamerR00M.Models
{
    public class PostViewModel
    {
        public PostModel PostInfo { get; set; }
        public List<CommentModel> CommentList { get; set; }
    }
}