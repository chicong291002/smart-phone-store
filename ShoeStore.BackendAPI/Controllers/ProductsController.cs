    using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPhoneStore.Application.Catalog.Products;
using SmartPhoneStore.ViewModels.Catalog.Products;
using System.Threading.Tasks;

namespace ShoeStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService manageProductService)
        {
            _productService = manageProductService;
        }

        //http://localhost:port/products?pageIndex=1&pageSize=10&CategoryIds=1
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllProductsPagings([FromQuery] GetProductPagingRequest request)
        {
            var products = await _productService.GetAllPagingProducts(request);
            return Ok(products);
        }

        //http://localhost:port/products/id
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var product = await _productService.getByProductId(productId);
            if (product == null)
            {
                return BadRequest("Cannot find Product");
            }
            return Ok(product);
        }

        [HttpGet("featured/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFeaturedProduct(int take)
        {
            var product = await _productService.GetFeaturedProducts(take);
            if (product == null)
            {
                return BadRequest("Cannot find Product");
            }
            return Ok(product);
        }

        [HttpGet("latest/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLatestProducts(int take)
        {
            var product = await _productService.GetFeaturedProducts(take);
            if (product == null)
            {
                return BadRequest("Cannot find Product");
            }
            return Ok(product);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]
        //chap nhan doi tuong form len 
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var productId = await _productService.Create(request);
            if (productId == 0)
                return BadRequest();

            var product = await _productService.getByProductId(productId);

            return CreatedAtAction(nameof(GetByProductId), new { id = productId }, product);
        }

        [HttpPut("{productId}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = productId;
            var product = await _productService.Update(request);
            if (product == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var productId = await _productService.Delete(id);
            if (productId == 0)
                return BadRequest();

            return Ok();
        }
    }
}
