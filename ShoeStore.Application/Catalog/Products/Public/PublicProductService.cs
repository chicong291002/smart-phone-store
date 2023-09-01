using Microsoft.EntityFrameworkCore;
using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.Catalog.Products.Manage;
using ShoeStore.Application.DTOS;
using ShoeStore.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Catalog.Products.Public
{
    public class PublicProductService : IPublicProductService
    {
        private readonly ShoeStoreDbContext _context; //readonly la chi gan 1 lan
        public PublicProductService(ShoeStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            // 1.Select join
            var query = from p in _context.Products
                        join
                             pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join
                             c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pic };

            var data = await query.Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Description = x.p.Description,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                }).ToListAsync();
            
            return data;
        }

        public async Task<PagedResult<ProductViewModel>> getAllByCategoryId(GetProductPagingRequest request)
        {
            // 1.Select join
            var query = from p in _context.Products
                        join
                             pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join
                             c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pic };

            //2 .filter
            if (request.CategoryIds.HasValue && request.CategoryIds.Value > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryIds);
            }

            // 3 Paging

            int totalRow = await query.CountAsync();
            var data = await query.Skip(request.pageIndex - 1).Take(request.pageSize).
                Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Description = x.p.Description,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                }).ToListAsync();
            //4 Select and projection
            var pageResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }
    }
}
