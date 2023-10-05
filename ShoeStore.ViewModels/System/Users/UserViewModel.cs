using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartPhoneStore.ViewModels.System.Users
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        [Display(Name="Tên")]
        public string firstName { get; set; }
        [Display(Name = "Họ")]
        public string lastName { get; set; }

        [Display(Name = "Số điện thoại")]
        public string phoneNumber { get; set; }
        [Display(Name = "Tài khoản")]
        public string userName { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Ngày Sinh")]
        public DateTime Dob { get; set; }

        public string Roles { get; set; }
    }
}
