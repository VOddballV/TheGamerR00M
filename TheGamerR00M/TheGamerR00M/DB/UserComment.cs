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
    
    public partial class UserComment
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public string CommentBody { get; set; }
        public int Comment_CUserID { get; set; }
        public System.DateTime Comment_CDate { get; set; }
    
        public virtual User User { get; set; }
        public virtual UsersPost UsersPost { get; set; }
    }
}