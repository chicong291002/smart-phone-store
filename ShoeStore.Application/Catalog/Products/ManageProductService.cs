using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.DTOS;
using ShoeStore.Data.EF;
using ShoeStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly ShoeStoreDbContext _context; //readonly la chi gan 1 lan
        public ManageProductService(ShoeStoreDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                Name = request.Name,
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync(); // ko can cho thread va phuc vu duoc request khac 
            //chay background ko can cho` 

        }

        public Task<int> Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> GetAllPagingProducts(string keyword, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(ProductUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
