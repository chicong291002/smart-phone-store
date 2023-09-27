using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Application.Common;
using ShoeStore.Data.EF;
using ShoeStore.Data.Entities;
using ShoeStore.ViewModels.Catalog.ProductImages;
using ShoeStore.ViewModels.Catalog.Products;
using ShoeStore.ViewModels.Common;
using System.Net.Http.Headers;

namespace ShoeStore.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly ShoeStoreDbContext _context; //readonly la chi gan 1 lan
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public ProductService(ShoeStoreDbContext context, IStorageService storageService)
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
                Stock = request.Stock,
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
                        ImagePath = await SaveFile(request.Image),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync(); // ko can cho thread va phuc vu duoc request khac 
            return product.Id;
            //chay background ko can cho` 
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

            var Image = _context.ProductImages.Where(i => i.ProductId == productId);

            foreach (var image in Image)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPagingProducts(GetProductPagingRequest request)
        {
            // 1.Select join
            var query = from p in _context.Products
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _context.Categories on pic.CategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        select new { p, pi, pic};

            //2 .filter

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.Name.Contains(request.Keyword));
            }

            if (request.CategoryId != null && request.CategoryId != 0)
            {
                query = query.Where(p => request.CategoryId == p.pic.CategoryId);
            }

            // 3 Paging

            int totalRow = await query.CountAsync();
            var data = await query.Skip(request.PageIndex - 1).Take(request.PageSize).
                Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Description = x.p.Description,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    Stock = x.p.Stock,
                    ThumbnailImage = x.pi.ImagePath
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
            product.Description = request.Description;

            //Save Image
            if (request.Image != null)
            {
                var Image = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true &&
                i.ProductId == request.Id);

                if (Image != null)
                {
                    Image.FileSize = request.Image.Length;
                    Image.ImagePath = await SaveFile(request.Image);
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
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ImagePath = i.ImagePath,
                    Caption = i.Caption,
                    IsDefault = i.IsDefault,
                    DateCreated = i.DateCreated,
                    SortOrder = i.SortOrder,
                    FileSize = i.FileSize
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
                productImage.ImagePath = await SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var Image = await _context.ProductImages.FindAsync(imageId);
            if (Image == null)
            {
                throw new Exception($"Cannot find a product: {Image}");
            }

            var productImages = _context.ProductImages.Where(i => i.Id == imageId);

            foreach (var image in productImages)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }
            _context.ProductImages.Remove(Image);
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
                image.ImagePath = await SaveFile(request.ImageFile);
                image.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Update(image);
            await _context.SaveChangesAsync();
            return image.Id;
        }

        public async Task<ProductViewModel> getByProductId(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            var image = await _context.ProductImages.Where(x => x.ProductId == productId && x.IsDefault == true).FirstOrDefaultAsync();
            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                DateCreated = product.DateCreated,
                Price = product.Price,
                OriginalPrice = product.OriginalPrice,
                Name = product.Name,
                Stock = product.Stock,
                Description = product.Description,
                ThumbnailImage = image != null ? image.ImagePath : "no-image.jpg"
            };

            return productViewModel;
        }

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            /* if (image != null)
             {
                 throw new Exception($"Cannot find an image with id {imageId}");
             }*/

            var productImageViewModel = new ProductImageViewModel()
            {
                Id = image.Id,
                ProductId = image.Id,
                ImagePath = image.ImagePath,
                Caption = image.Caption,
                IsDefault = image.IsDefault,
                DateCreated = image.DateCreated,
                SortOrder = image.SortOrder,
                FileSize = image.FileSize,
            };

            return productImageViewModel;
        }

        public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<bool>($"Sản phẩm với id {id} không tồn tại");
            }

            foreach (var category in request.Categories)
            {
                var productInCategory = await _context.ProductInCategories
                    .FirstOrDefaultAsync(x => x.CategoryId == int.Parse(category.Id)
                    && x.ProductId == id);
                if (productInCategory != null && category.Selected == false)
                {
                    _context.ProductInCategories.Remove(productInCategory);
                }
                else if (productInCategory == null && category.Selected)
                {
                    await _context.ProductInCategories.AddAsync(new ProductInCategory()
                    {
                        CategoryId = int.Parse(category.Id),
                        ProductId = id
                    });
                }
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<List<ProductViewModel>> GetFeaturedProducts(int take)
        {
            // 1.Select join
            var query = from p in _context.Products
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _context.Categories on pic.CategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        join pi in _context.ProductImages.Where(x=> x.IsDefault == true)    
                        on p.Id equals pi.ProductId into ppi          
                        from pi in ppi.DefaultIfEmpty()
                        select new { p, pi, pic };


            var data = await query.OrderByDescending(x=>x.p.DateCreated).Take(take).
            Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Description = x.p.Description,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    Stock = x.p.Stock,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();
         
            return data;
        }
    }
}
