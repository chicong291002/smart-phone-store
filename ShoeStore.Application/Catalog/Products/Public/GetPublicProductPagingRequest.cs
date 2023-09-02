using ShoeStore.Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Catalog.Products.Public
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        // thuoc tinh rieng
        public int? CategoryIds { get; set; }
    }
}
