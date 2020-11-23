﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Web.Models
{
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Bills = new HashSet<Bill>();
            this.Stores = new HashSet<Store>();
            this.User_Roles = new HashSet<User_Roles>();
        }
    
        public string IDUser { get; set; }
        [MinLength(8, ErrorMessage = "Tên ít nhất 8 kí tự")]
        [RegularExpression(@"^(([A-za-z0-9]+[\s]{1}[A-za-z0-9]+)|([A-Za-z0-9]+))$", ErrorMessage = "Tên của bạn phải chứa chữ cái viết thường hoặc hoa, VD: Huutuong")]
        public string Username { get; set; }
        //[MinLength(8, ErrorMessage = "Mật khẩu phải nhập ít nhất 8 kí tự")]
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Mật khẩu phải chứa chữ in hoa, số hoặc ký tự đặc biệt")]
        //[Required(ErrorMessage = "Mật khẩu không được trống!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Họ tên không được trống!")]
        public string Fullname { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateofBirth { get; set; }
        public string IdentityCard { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được trống")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được trống")]
        [RegularExpression(@"(03|07|08|09|01[2|6|8|9])+([0-9]{8})", ErrorMessage = "Hãy nhập số điện thoại hợp lệ, VD:03xxxxxxxx")]
        public string PhoneNumber { get; set; }
        public Nullable<double> Point { get; set; }
        [Required(ErrorMessage = "Email không được trống")]
        [RegularExpression(@"\A(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                            ErrorMessage = "Hãy nhập email hợp lệ, VD: huutuong@gmail.com")]
        public string Email { get; set; }
        public string Image { get; set; }
        public string Facebook { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Store> Stores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Roles> User_Roles { get; set; }
    }
}
