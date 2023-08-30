using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Catalog.Products
{
    public interface IPublicProductService
    {
       PagedViewModel<ProductViewModel> getAllByCategoryId(int categoryId,int pageIndex, int pageSize);
    }
}
