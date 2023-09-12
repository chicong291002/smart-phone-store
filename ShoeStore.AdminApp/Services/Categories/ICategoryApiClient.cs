using ShoeStore.Application.Catalog.Categories.DTOS;
using ShoeStore.Application.Common;
using ShoeStore.Data.Entities;

namespace ShoeStore.AdminApp.Services.Categories
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryViewModel>> GetAllCategorys();

        Task<CategoryViewModel> GetById(int id);
    }
}
