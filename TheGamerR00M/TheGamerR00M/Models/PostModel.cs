using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheGamerR00M.Models
{
    public class PostModel
    {
        public int PostID { get; set; }
        public int PostTypeID { get; set; }
        public string PostTitle { get; set; }
        public string PostImageURL { get; set; }
        public string PostBody { get; set; }
        public DateTime Post_CDate { get; set; }
        public int Post_CUserID { get; set; }
        public string PostTags { get; set; }
        public string Post_Author { get; set; }
    }
}