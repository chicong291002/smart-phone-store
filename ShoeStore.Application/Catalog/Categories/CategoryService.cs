using Microsoft.EntityFrameworkCore;
using ShoeStore.Application.Common;
using ShoeStore.Data.EF;
using ShoeStore.Data.Entities;
using ShoeStore.ViewModels.Catalog.Categories;
using ShoeStore.ViewModels.Catalog.Products;
using ShoeStore.ViewModels.Common;

namespace ShoeStore.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ShoeStoreDbContext _context; //readonly la chi gan 1 lan
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public CategoryService(ShoeStoreDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<List<CategoryViewModel>> GetAllCategorys()
        {
            // 1.Select join
            var query = from c in _context.Categories
                        select new { c } ;

            int totalRow = await query.CountAsync();
            return await query.Select(
                x => new CategoryViewModel()
                {
                    Id = x.c.Id,
                    Name = x.c.Name,
                    ParentId = x.c.ParentId
                }).ToListAsync();
        }

        public async Task<int> Create(CategoryCreateRequest request)
        {
            var category = new Category()
            {
                Name = request.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync(); // ko can cho thread va phuc vu duoc request khac 
            return category.Id; 
        }

        public Task<int> Update(CategoryUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> getByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
