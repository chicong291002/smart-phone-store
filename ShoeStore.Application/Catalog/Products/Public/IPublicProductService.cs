using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.Catalog.Products.Manage;
using ShoeStore.Application.DTOS;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Catalog.Products.Public
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> getAllByCategoryId(GetProductPagingRequest request);
    }
}
