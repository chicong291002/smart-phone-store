using Microsoft.AspNetCore.Http;
using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.DTOS;

namespace ShoeStore.Application.Catalog.Products.Manage
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(ProductDeleteRequest request);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<List<ProductViewModel>> GetAllProducts();
        //trả về một model mà có đầy đủ các thông tin 
        Task<PagedResult<ProductViewModel>> GetAllPagingProducts(GetManageProductPagingRequest request);
        Task<int> AddImage(int productId, ProductImageCreateRequest request);
        Task<int> RemoveImage(int imageId);
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);
        Task<List<ProductImageViewModel>> GetListImage(int productId);
    }
}
