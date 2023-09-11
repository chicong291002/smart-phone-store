using Microsoft.EntityFrameworkCore;
using ShoeStore.Application.Catalog.Categories.DTOS;
using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.Common;
using ShoeStore.Application.DTOS;
using ShoeStore.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
