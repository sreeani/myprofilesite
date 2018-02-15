using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class WorkViewModel : UserViewModel
    {
        public int WorkID { get; set; }

        public String Role { get; set; }

        public String Duration { get; set; }

        public String Duties { get; set; }

        public String ReasonForLeaving { get; set; }

        public String  Expertise  { get; set; }

        public String Location { get; set; }

        [Display (Name="Company")]
        public String company { get; set; }

        public String Accomplishments { get; set; }
    }
}