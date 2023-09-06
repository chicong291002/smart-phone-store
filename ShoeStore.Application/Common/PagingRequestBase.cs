using ShoeStore.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.DTOS
{
    public class PagingRequestBase
    {
        public int pageIndex { get; set; } //vi tri lay trang so bao nhieu
        public int pageSize { get; set; } //kich co~ cua trang  
    }
}
