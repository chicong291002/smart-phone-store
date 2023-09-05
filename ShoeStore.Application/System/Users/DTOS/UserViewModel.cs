using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.System.Users.DTOS
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string phoneNumber { get; set; } 

        public string userName { get; set; }    
        public string email { get; set; }
    }
}
