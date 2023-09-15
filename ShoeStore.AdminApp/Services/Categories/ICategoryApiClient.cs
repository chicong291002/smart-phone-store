
using ShoeStore.Data.Entities;
using ShoeStore.ViewModels.Catalog.Categories;
using ShoeStore.ViewModels.Catalog.Products;
using ShoeStore.ViewModels.Common;

namespace ShoeStore.AdminApp.Services.Categories
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryViewModel>> GetAllCategorys();

        Task<CategoryViewModel> GetById(int id);

        Task<bool> CreateCategory(CategoryCreateRequest request);

        Task<bool> UpdateCategory(CategoryUpdateRequest request);

        Task<bool> DeleteCategory(int id);

        Task<PagedResult<CategoryViewModel>> GetAllCategoryPaging(GetProductPagingRequest request);
    }
}
