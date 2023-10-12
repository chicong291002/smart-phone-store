using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.ViewModels.Catalog.Coupons
{
    public class CouponCreateRequest
    {
        public string Code { get; set; }
        public int Count { get; set; }
        public int Promotion { get; set; }
        public string Describe { get; set; }
    }
}
