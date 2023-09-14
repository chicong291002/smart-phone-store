
using ShoeStore.Data.Entities;
using ShoeStore.ViewModels.Catalog.Categories;

namespace ShoeStore.AdminApp.Services.Categories
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryViewModel>> GetAllCategorys();

        Task<CategoryViewModel> GetById(int id);
    }
}
