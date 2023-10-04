using System;
using System.ComponentModel.DataAnnotations;

namespace SmartPhoneStore.ViewModels.System.Users
{
    public class RegisterRequest
    {
        [Display(Name = "Tên")]
        public string firstName { get; set; }
        [Display(Name = "Họ")]
        public string lastName { get; set; }
        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        [Display(Name = "Hòm Thư")]
        public string email { get; set; }

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