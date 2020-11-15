﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel.DataAnnotations;

    public partial class Store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Store()
        {
            this.Bills = new HashSet<Bill>();
            this.Menu_Stores = new HashSet<Menu_Stores>();
            this.Statistics = new HashSet<Statistic>();
            this.Warehouses = new HashSet<Warehouse>();
        }
    
        public string IDStore { get; set; }
        [Required(ErrorMessage = "Tên cửa hàng không được trống!")]
        public string StoreName { get; set; }
        public string Location { get; set; }
        public string Promotion { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được trống")]
        [RegularExpression(@"(03|07|08|09|01[2|6|8|9])+([0-9]{8})", ErrorMessage = "Hãy nhập số điện thoại hợp lệ, VD:03xxxxxxxx")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email không được trống")]
        [RegularExpression(@"\A(?:A-Z[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                            ErrorMessage = "Hãy nhập email hợp lệ, VD: huutuong@gmail.com")]
        public string Email { get; set; }
        public string IDUser { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menu_Stores> Menu_Stores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Statistic> Statistics { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Warehouse> Warehouses { get; set; }
    }
}
