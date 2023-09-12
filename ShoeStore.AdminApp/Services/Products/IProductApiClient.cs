
using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.Common;
using ShoeStore.Application.DTOS;
using ShoeStore.Application.System.Users.DTOS;

namespace ShoeStore.AdminApp.Services.Products
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductViewModel>> GetAllProductsPaging(GetProductPagingRequest request);
        Task<bool> CreateProduct(ProductCreateRequest request);

        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);

        Task<ProductViewModel> GetByProductId(int id);
    }   
}
