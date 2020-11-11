using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Project_Web.Models
{
    public class CreateSeller
    {
        [MinLength(8, ErrorMessage = "Tên ít nhất 8 kí tự")]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Tên của bạn phải chứa chữ cái viết thường hoặc hoa, VD: Huutuong")]
        public string Username { get; set; }
        [MinLength(8, ErrorMessage = "Mật khẩu phải nhập ít nhất 8 kí tự")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Mật khẩu phải chứa chữ in hoa, số hoặc ký tự đặc biệt")]
        [Required(ErrorMessage = "Mật khẩu không được trống!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Họ tên không được trống!")]
        public string Fullname
        {
            get; set;
        }
        [Required(ErrorMessage = "Địa chỉ không được trống")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được trống")]
        [RegularExpression(@"(03|07|08|09|01[2|6|8|9])+([0-9]{8})", ErrorMessage = "Hãy nhập số điện thoại hợp lệ, VD:03xxxxxxxx")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email không được trống")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                           ErrorMessage = "Hãy nhập email hợp lệ, VD: huutuong@gmail.com")]
        public string Email { get; set; }
        public string Role { get; set; }
    }

}