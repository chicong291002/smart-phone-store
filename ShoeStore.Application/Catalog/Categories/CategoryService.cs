using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Data.EF;
using ShoeStore.Data.Entities;
using ShoeStore.ViewModels.Catalog.Categories;
using ShoeStore.ViewModels.Catalog.Products;
using ShoeStore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoeStore.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ShoeStoreDbContext _context; //readonly la chi gan 1 lan

        public CategoryService(ShoeStoreDbContext context)
        {
            _context = context;
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
            _context.Categories.Update(category);

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

        public async Task<CategoryViewModel> getByCategoryId(int id)
        {
            var query = from c in _context.Categories
                        where c.Id == id
                        select new { c };

            return await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.Id,
                Name = x.c.Name
            }).FirstOrDefaultAsync();
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            var query = from c in _context.Categories
                        select new { c };

            return await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.Id,
                Name = x.c.Name,
            }).ToListAsync();
        }

        public async Task<PagedResult<CategoryViewModel>> GetAllCategoryPaging(GetProductPagingRequest request)
        {
            var query = from c in _context.Categories
                        join subc in _context.Subcategories on c.Id equals subc.CategoryId into subcategories
                        select new { Category = c, Subcategories = subcategories };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.Category.Name.Contains(request.Keyword));

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name,
                    Subcategories = x.Subcategories.Select(s => new Subcategory
                    {
                        SubcategoryId = s.SubcategoryId,
                        SubcategoryName = s.SubcategoryName,
                    }).ToList()
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
