using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class ProjectViewModel : UserViewModel
    {
        public int PersonID { get; set; }

        public int ProjectID { get; set; }

        [Display(Name = "ProjectOn")]
        public String ProjectName { get; set; }

        public String ProjectDescription { get; set; }

        public int TeamSize { get; set; }

        public String Role { get; set; }
    }
}