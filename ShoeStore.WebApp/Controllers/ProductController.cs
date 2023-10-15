using Microsoft.AspNetCore.Mvc;
using SmartPhoneStore.AdminApp.ApiIntegration.Categories;
using SmartPhoneStore.AdminApp.ApiIntegration.Products;
using SmartPhoneStore.ViewModels.Catalog.Products;
using SmartPhoneStore.WebApp.Models;
using System.Threading.Tasks;

namespace ShoeStore.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productApiClient.GetByProductId(id);
            return View(new ProductDetailViewModel()
            {
                Product = product,
                Category = await _categoryApiClient.GetById(id)
            });
        }

        public async Task<IActionResult> Category(int id, int pageIndex = 1)
        {
            var products = await _productApiClient.GetAllProductsPaging(new GetProductPagingRequest()
            {
                CategoryId = id,
                PageIndex = pageIndex,
                PageSize = 10
            });

            return View(new ProductCategoryViewModel()
            {
                Category = await _categoryApiClient.GetById(id),
                Products = products
            });
        }
    }
}
