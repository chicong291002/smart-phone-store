using SmartPhoneStore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.ViewModels.System.Users
{
    public class GetUserPagingRequest : PageResultBase
    {
        public string keyword { get; set; }
    }
}
