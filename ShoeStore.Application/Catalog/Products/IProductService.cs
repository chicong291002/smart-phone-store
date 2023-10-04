using SmartPhoneStore.ViewModels.Catalog.ProductImages;
using SmartPhoneStore.ViewModels.Catalog.Products;
using SmartPhoneStore.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartPhoneStore.Application.Catalog.Products
{
    public interface IProductService    
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int productId);
        Task<PagedResult<ProductViewModel>> GetAllPagingProducts(GetProductPagingRequest request);

        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request);
        Task<ProductViewModel> getByProductId(int productId);

        Task<List<ProductViewModel>> GetFeaturedProducts(int take);

        Task<List<ProductViewModel>> GetLatestProducts(int take);
    }
}
