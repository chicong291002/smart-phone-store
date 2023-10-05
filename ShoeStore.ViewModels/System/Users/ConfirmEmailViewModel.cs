using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.ViewModels.System.Users
{
    public class ConfirmEmailViewModel
    {
        public string token { get; set; }
        public string email { get; set; }
    }
}
