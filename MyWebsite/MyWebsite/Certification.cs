
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace MyWebsite
{

using System;
    using System.Collections.Generic;
    
public partial class Certification
{

    public int PersonID { get; set; }

    public int CertificationID { get; set; }

    public string CertificationNumber { get; set; }

    public string CertificationName { get; set; }

    public System.DateTime Isuuedate { get; set; }

    public string ExpirationDate { get; set; }

    public string IssuingOrganisation { get; set; }

    public string Location { get; set; }

    public string Experience { get; set; }



    public virtual Person Person { get; set; }

}

}
