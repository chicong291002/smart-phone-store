using Azure.Core;
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

        public async Task<int> Update(CategoryUpdateRequest request)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            if (category == null)
            {
                throw new Exception($"Cannot find a category: {request.Id}");
            }
            category.Name = request.Name;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (category == null)
            {
                throw new Exception($"Cannot find a category: {categoryId}");
            }

            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync();
        }

        public async Task<CategoryViewModel> getByCategoryId(int categoryId)
        {
            var query = from c in _context.Categories where c.Id == categoryId
                        select new { c };

            return await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.Id,
                Name = x.c.Name,
            }).FirstOrDefaultAsync();
        }

        public async Task<PagedResult<CategoryViewModel>> GetAllCategoryPaging(GetProductPagingRequest request)
        {
            var query = from c in _context.Categories
                        select new { c };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.c.Name.Contains(request.Keyword));

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CategoryViewModel()
                {
                    Id = x.c.Id,
                    Name = x.c.Name,
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<CategoryViewModel>()
            {
                TotalRecord = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }
    }
}
