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

    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            this.Menu_Stores = new HashSet<Menu_Stores>();
        }
    
        public string IDDish { get; set; }
        [Required(ErrorMessage = "Tên món ăn không được trống!")]
        public string DishName { get; set; }
        public string Ingredient { get; set; }
        public Nullable<double> ImportPrice { get; set; }
        public Nullable<double> SalePrice { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menu_Stores> Menu_Stores { get; set; }
    }
}
