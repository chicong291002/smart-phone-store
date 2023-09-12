using ShoeStore.Application.Catalog.Categories;
using ShoeStore.Data.Entities;

namespace ShoeStore.AdminApp.Services.Categories
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryViewModel>> GetAllCategorys();
    }
}
