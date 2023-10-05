using System;
using System.ComponentModel.DataAnnotations;

namespace SmartPhoneStore.ViewModels.System.Users
{
    public class RegisterRequest
    {
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }


        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Số điện thoại")]
        public string phoneNumber { get; set; }
        [Display(Name = "Tài khoản ")]
        public string userName { get; set; }
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string passWord { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }
    }
}