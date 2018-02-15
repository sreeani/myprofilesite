using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebsite.Models
{
    public class EducationViewModel: UserViewModel
    {
        public int EducationID { get; set; }

        public int PersonID { get; set; }

        [Display(Name ="Degree")]
        public string DegreeName { get; set; }

        [Display(Name="Graduation Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")] 
        public DateTime GraduationDate { get; set; }

        public string Major { get; set; }

        [Display(Name = "GPA")]
        public decimal Percentage { get; set; }

        [Display(Name = "College")]
        public string InstitutionName { get; set; }

        [Display(Name = "Concentrations")]
        public string SpecializedConcentrations { get; set; }
    }
}