using ShoeStore.Application.Common;
using ShoeStore.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Catalog.Products.DTOS
{
    public class GetProductPagingRequest : PageResultBase
    {
        public string Keyword { get; set; }
        public int? CategoryId { get; set; }
    }
}
