using ShoeStore.ViewModels.Catalog.Categories;
using ShoeStore.ViewModels.Catalog.Products;

namespace ShoeStore.Application.Catalog.Categories
{
    public interface ICategoryService
    {   
        Task<List<CategoryViewModel>> GetAllCategorys();

        Task<int> Create(CategoryCreateRequest request);
        Task<int> Update(CategoryUpdateRequest request);
        Task<int> Delete(int categoryId);
        Task<ProductViewModel> getByCategoryId(int categoryId);
    }
}
