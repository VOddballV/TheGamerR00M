//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheGamerR00M.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsersPost
    {
        public UsersPost()
        {
            this.UserComments = new HashSet<UserComment>();
        }
    
        public int PostID { get; set; }
        public int PostTypeID { get; set; }
        public string PostTitle { get; set; }
        public string PostImageURL { get; set; }
        public string PostBody { get; set; }
        public System.DateTime Post_CDate { get; set; }
        public int Post_CUserID { get; set; }
        public string PostTags { get; set; }
    
        public virtual User User { get; set; }
        public virtual ICollection<UserComment> UserComments { get; set; }
    }
}