using SmartPhoneStore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.ViewModels.Catalog.Orders
{
    public class GetOrderPagingRequest : PageResultBase
    {
        public string Keyword { get; set; }
        public string? SortOption { get; set; }
    }
}
