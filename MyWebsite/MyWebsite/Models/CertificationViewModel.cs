using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class CertificationViewModel : UserViewModel
    {
        public int PersonID { get; set; }

        public int CertificationID { get; set; }

        public string CertificationNumber { get; set; }

        public String CertificationName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime IssueDate { get; set; }

        public String ExpirationDate { get; set; }

        public String IssuingOrganisation { get; set; }

        public String Location { get; set; }

        public String Experience { get; set; }
    }
}