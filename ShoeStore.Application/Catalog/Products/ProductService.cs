using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SmartPhoneStore.Application.Common;
using SmartPhoneStore.Data.EF;
using SmartPhoneStore.Data.Entities;
using SmartPhoneStore.Utilities.Constants;
using SmartPhoneStore.ViewModels.Catalog.Products;
using SmartPhoneStore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SmartPhoneStore.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly SmartPhoneStoreDbContext _context; //readonly la chi gan 1 lan
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "Images";

        public ProductService(SmartPhoneStoreDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                CategoryId = request.CategoryId,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                DateCreated = DateTime.Now,
            };

            //Save Image
            if (request.ThumbnailImage != null)
            {
                product.Thumbnail = await this.SaveFile(request.ThumbnailImage);
            }
            else
            {
                product.Thumbnail = "/Images/no-image.png";
            }

            //Save product image
            if (request.ProductImage != null)
            {
                product.ProductImage = await this.SaveFile(request.ProductImage);
            }
            else
            {
                product.ProductImage = "/Images/no-image.png";
            }

            _context.Products.Add(product); 
            await _context.SaveChangesAsync();
            return product.Id;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                throw new Exception($"Cannot find a product: {productId}");
            }

            var images = _context.Products.Where(i => i.Id == productId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.Thumbnail);
                await _storageService.DeleteFileAsync(image.ProductImage);
            }

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPagingProducts(GetProductPagingRequest request)
        {
            // 1.Select join
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        select new { p };

            //2 .filter

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.Name.Contains(request.Keyword));
            }

            if (request.CategoryId != null && request.CategoryId != 0)
            {
                query = query.Where(x => x.p.CategoryId == request.CategoryId);
            }

            // 3 Paging

            int totalRow = await query.CountAsync();
            var data = await query.Skip(request.PageIndex - 1).Take(request.PageSize).
                Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    CategoryId = x.p.CategoryId,
                    Name = x.p == null ? SystemConstants.ProductConstants.NA : x.p.Name,
                    Description = x.p == null ? SystemConstants.ProductConstants.NA : x.p.Description,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    Stock = x.p.Stock,
                    ThumbnailImage = x.p.Thumbnail,
                    ProductImage = x.p.ProductImage
                }).ToListAsync();
            //4 Select and projection
            var pageResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pageResult;
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null)
            {
                throw new Exception($"Cannot find a product: {request.Id}");
            }
            product.Name = request.Name;
            product.CategoryId = request.CategoryId;
            product.Description = request.Description;

            //Save Image
            if (request.ThumbnailImage != null)
            {
                product.Thumbnail = await this.SaveFile(request.ThumbnailImage);
            }
            else
            {
                product.Thumbnail = "/Images/no-image.png";
            }

            //Save product image
            if (request.ProductImage != null)
            {
                product.ProductImage = await this.SaveFile(request.ProductImage);
            }
            else
            {
                product.ProductImage = "/Images/no-image.png";
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<ProductViewModel> getByProductId(int productId)
        {
            // Lấy danh mục của sản phẩm
            var categories = await (from c in _context.Categories
                                    join p in _context.Products on c.Id equals p.CategoryId
                                    select p.Name).ToListAsync();

            var product = await _context.Products.FindAsync(productId);

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                DateCreated = product.DateCreated,
                CategoryId = product.CategoryId != 0 ? product.CategoryId : 0,
                Price = product.Price,
                OriginalPrice = product.OriginalPrice,
                Name = product.Name != null ? product.Name : null,
                Stock = product.Stock,
                Description = product.Description,
                ThumbnailImage = product.Thumbnail != null ? product.Thumbnail : "no-image.jpg",
                ProductImage = product.ProductImage != null ? product.ProductImage : "no-image.jpg",
            };

            return productViewModel;
        }


        public async Task<List<ProductViewModel>> GetFeaturedProducts(int take)
        {
            // 1.Select join
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id into picc
                        select new { p };


            var data = await query.OrderByDescending(x => x.p.DateCreated).Take(take).
            Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                CategoryId = x.p.CategoryId,
                Description = x.p.Description,
                OriginalPrice = x.p.OriginalPrice,
                Price = x.p.Price,
                Stock = x.p.Stock,
                ThumbnailImage = x.p.Thumbnail,
                ProductImage = x.p.ProductImage,
            }).ToListAsync();

            return data;
        }

        public async Task<List<ProductViewModel>> GetLatestProducts(int take)
        {
            // 1.Select join
            var query = from p in _context.Products
                            join c in _context.Categories on p.CategoryId equals c.Id into picc
                        select new { p };

            var data = await query.OrderByDescending(x => x.p.DateCreated).Take(take).
            Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                CategoryId = x.p.CategoryId,
                Description = x.p.Description,
                OriginalPrice = x.p.OriginalPrice,
                Price = x.p.Price,
                Stock = x.p.Stock,
                ThumbnailImage = x.p.Thumbnail,
                ProductImage = x.p.ProductImage,
            }).ToListAsync();

            return data;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id into picc
                        select new { p };
            //2. filter
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.p.CategoryId == request.CategoryId);
            }
            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    CategoryId = x.p.CategoryId,
                    //Category = x.p.Category,
                    Description = x.p.Description,
                    Price = x.p.Price,
                    Stock = x.p.Stock,
                    ThumbnailImage = x.p.Thumbnail,
                    ProductImage = x.p.ProductImage
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
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
