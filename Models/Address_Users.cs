//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Address_Users
    {
        public string IDAddress { get; set; }
        public string IDUser { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public Nullable<int> IsDefault { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public string Fullname { get; set; }
    
        public virtual User User { get; set; }
    }
}
