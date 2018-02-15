using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}