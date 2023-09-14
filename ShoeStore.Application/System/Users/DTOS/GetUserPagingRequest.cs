using ShoeStore.Application.Common;
using ShoeStore.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.System.Users.DTOS
{
    public class GetUserPagingRequest : PageResultBase
    {
        public string keyword { get; set; }
    }
}
