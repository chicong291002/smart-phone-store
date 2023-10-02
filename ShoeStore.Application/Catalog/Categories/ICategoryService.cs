using ShoeStore.ViewModels.Catalog.Categories;
using ShoeStore.ViewModels.Catalog.Products;
using ShoeStore.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoeStore.Application.Catalog.Categories
{
    public interface ICategoryService
    {   
        Task<PagedResult<CategoryViewModel>> GetAllCategoryPaging(GetProductPagingRequest request);
        Task<int> Create(CategoryCreateRequest request);
        Task<int> Update(CategoryUpdateRequest request);
        Task<int> Delete(int categoryId);
        Task<CategoryViewModel> getByCategoryId(int categoryId);
        Task<List<CategoryViewModel>> GetAll();
    }
}
