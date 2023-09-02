using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Catalog.Products.Manage;
using ShoeStore.Application.Catalog.Products.Public;

namespace ShoeStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;

        public ProductController(IPublicProductService publicProductService)
        {
            _publicProductService = publicProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _publicProductService.GetAll();
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByCategory([FromQuery]GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.getAllByCategoryId(request);
            return Ok(products);
        }
    }
}
