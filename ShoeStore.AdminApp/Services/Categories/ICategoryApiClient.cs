using ShoeStore.Application.Catalog.Categories.DTOS;
using ShoeStore.Application.Common;

namespace ShoeStore.AdminApp.Services.Categories
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryViewModel>> GetAllCategorys();
    }
}
