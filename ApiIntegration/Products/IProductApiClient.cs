using SmartPhoneStore.ViewModels.Catalog.Products;
using SmartPhoneStore.ViewModels.Common;

namespace SmartPhoneStore.AdminApp.ApiIntegration.Products
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductViewModel>> GetAllProductsPaging(GetProductPagingRequest request);
        Task<bool> CreateProduct(ProductCreateRequest request);

        Task<bool> Update(ProductUpdateRequest request);

        Task<bool> Delete(int id);
        Task<ProductViewModel> GetByProductId(int id);
        Task<bool> CategoryAssign(int id, CategoryAssignRequest request);

        Task<List<ProductViewModel>> GetFeaturedProducts(int take);

        Task<List<ProductViewModel>> GetLatestProducts(int take);
    }
}
