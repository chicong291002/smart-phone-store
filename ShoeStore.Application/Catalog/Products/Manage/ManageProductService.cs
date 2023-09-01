﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.Common;
using ShoeStore.Application.DTOS;
using ShoeStore.Data.EF;
using ShoeStore.Data.Entities;
using System;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace ShoeStore.Application.Catalog.Products.Manage
{
    public class ManageProductService : IManageProductService
    {
        private readonly ShoeStoreDbContext _context; //readonly la chi gan 1 lan
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public ManageProductService(ShoeStoreDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                DateCreated = DateTime.Now,

            };

            //Save Image
            if (request.Image != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Image",
                        DateCreated  = DateTime.Now,
                        FileSize = request.Image.Length,
                        ImagePath = await this.SaveFile(request.Image),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Products.Add(product);
            return await _context.SaveChangesAsync(); // ko can cho thread va phuc vu duoc request khac 
            //chay background ko can cho` 
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<int> Delete(ProductDeleteRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null)
            {
                throw new Exception($"Cannot find a product: {request.Id}");
            }

            var Image = _context.ProductImages.Where(i => i.ProductId == request.Id);

            foreach (var image in Image)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPagingProducts(GetManageProductPagingRequest request)
        {
            // 1.Select join
            var query = from p in _context.Products
                        join
                        pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join
                        c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pic };

            //2 .filter

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.Name.Contains(request.Keyword));
            }

            if (request.CategoryIds > 0)
            {
                query = query.Where(p => request.CategoryIds == p.pic.CategoryId);
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

        public async Task<List<ProductViewModel>> GetAllProducts()
        {
            return await _context.Products.Select(i => new ProductViewModel()
            {
                Price = i.Price,
                OriginalPrice = i.OriginalPrice,
                DateCreated = i.DateCreated,
                Name = i.Name,
                Description = i.Description,
                ThumbnailImage = i.Thumbnail,
            }).ToListAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null)
            {
                throw new Exception($"Cannot find a product: {request.Id}");
            }
            product.Name = request.Name;
            product.Description = request.Description;

            //Save Image
            if (request.Image != null)
            {
                var Image = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true &&
                i.ProductId == request.Id);

                if (Image != null)
                {
                    Image.FileSize = request.Image.Length;
                    Image.ImagePath = await this.SaveFile(request.Image);
                    _context.ProductImages.Update(Image);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception($"Cannot find a product: {productId}");
            }

            product.Price = newPrice;
            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId).
                Select(i => new ProductImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    IsDefault = i.IsDefault,
                    ProductId = i.ProductId,
                    SortOrder = i.SortOrder
                }).ToListAsync();
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception($"Cannot find a product: {productId}");
            }
            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                IsDefault = true,
                ProductId = productId,
                SortOrder = request.SortOrder,
            };

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
            {
                throw new Exception($"Cannot find a product: {image}");
            }

            _context.ProductImages.Remove(image);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
            {
                throw new Exception($"Cannot find a image: {request.Id}");
            }

            //Save Image
            if (request.ImageFile != null)
            {
                image.ImagePath = await this.SaveFile(request.ImageFile);
                image.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Update(image);
            return await _context.SaveChangesAsync();
        }
    }
}