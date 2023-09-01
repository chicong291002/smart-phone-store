using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.DTOS
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } //generic
        public int TotalRecord { set;get; }

    }
}
