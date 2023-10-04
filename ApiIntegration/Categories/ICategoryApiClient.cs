
using SmartPhoneStore.Data.Entities;
using SmartPhoneStore.ViewModels.Catalog.Categories;
using SmartPhoneStore.ViewModels.Catalog.Products;
using SmartPhoneStore.ViewModels.Common;

namespace SmartPhoneStore.AdminApp.ApiIntegration.Categories
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
