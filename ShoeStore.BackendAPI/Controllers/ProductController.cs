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
        private readonly IProductService _manageProductService;

        public ProductController(IProductService manageProductService)
        {
            _manageProductService = manageProductService;
        }

        //http://localhost:port/product
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _manageProductService.GetAllProducts();
            return Ok(products);
        }

        //http://localhost:port/product/public-paging
        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery] GetManageProductPagingRequest request)
        {
            var products = await _manageProductService.GetAllPagingProducts(request);
            return Ok(products);
        }

        //http://localhost:port/product/id
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var product = await _manageProductService.getByProductId(productId);
            if (product == null)
            {
                return BadRequest("Cannot find Product");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            var productId = await _manageProductService.Create(request);
            /*if (productId == 0)
                return BadRequest();*/

            var product = await _manageProductService.getByProductId(productId);

            return CreatedAtAction(nameof(GetByProductId), new { id = productId }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            var productId = await _manageProductService.Update(request);
            if (productId == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] ProductDeleteRequest request)
        {
            var productId = await _manageProductService.Delete(request);
            if (productId == 0)
                return BadRequest();

            return Ok();
        }

        [HttpPut("price/{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var successful = await _manageProductService.UpdatePrice(productId, newPrice);
            if (successful)
                return Ok();
            return BadRequest();
        }
    }
}
