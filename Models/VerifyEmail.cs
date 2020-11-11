using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Web.Models
{
    public class VerifyEmail
    {
        [Required(ErrorMessage = "Email không được trống")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                           ErrorMessage = "Hãy nhập email hợp lệ, VD: huutuong@gmail.com")]
        public string Email { get; set; }
    }
}