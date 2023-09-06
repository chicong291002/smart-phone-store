using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.System.Users.DTOS
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
    }
}
