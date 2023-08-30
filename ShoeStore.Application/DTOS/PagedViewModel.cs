using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.DTOS
{
    public class PagedViewModel<T>
    {
        List<T> Items { get; set; } //generic
        public int TotalRecord { set;get; }

    }
}
