using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.DTOS;

namespace ShoeStore.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int productId);

        Task<List<ProductViewModel>> GetAllProducts();


        //trả về một model mà có đầy đủ các thông tin 
        Task<ProductViewModel> GetAllPagingProducts(string keyword, int pageIndex, int pageSize);
    }
}
