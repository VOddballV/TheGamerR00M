using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheGamerR00M.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPass { get; set; }
        public int UserRankID { get; set; }
        public int UserStatusID { get; set; }
    }
}