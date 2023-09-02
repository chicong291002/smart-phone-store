using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.DTOS;

namespace ShoeStore.Application.Catalog.Products.Public
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> getAllByCategoryId(GetPublicProductPagingRequest request);

        Task<List<ProductViewModel>> GetAll();
    }
}
