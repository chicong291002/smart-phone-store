using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.System.Users.DTOS
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
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
    }
}
