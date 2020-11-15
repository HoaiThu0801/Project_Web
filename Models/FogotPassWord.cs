using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Web.Models
{
    public class FogotPassWord
    {
        [MinLength(8, ErrorMessage = "Tên ít nhất 8 kí tự")]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Tên của bạn phải chứa chữ cái viết thường hoặc hoa, VD: Huutuong")]
        public string Username { get; set; }
        [MinLength(8, ErrorMessage = "Mật khẩu phải nhập ít nhất 8 kí tự")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Mật khẩu phải chứa chữ in hoa, số hoặc ký tự đặc biệt")]
        [Required(ErrorMessage = "Mật khẩu không được trống!")]
        public string Password { get; set; }
    }
}