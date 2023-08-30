using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        public PagedViewModel<ProductViewModel> getAllByCategoryId(int categoryId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
