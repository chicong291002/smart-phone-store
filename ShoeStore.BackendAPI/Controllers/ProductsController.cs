using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Catalog.ProductImages;
using ShoeStore.Application.Catalog.Products;
using ShoeStore.Application.Catalog.Products.DTOS;

namespace ShoeStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _manageProductService;

        public ProductsController(IProductService manageProductService)
        {
            _manageProductService = manageProductService;
        }

        //http://localhost:port/products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _manageProductService.GetAllProducts();
            return Ok(products);
        }

        //http://localhost:port/products?pageIndex=1&pageSize=10&CategoryIds=1
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllProductsPagings([FromQuery] GetManageProductPagingRequest request)
        {
            var products = await _manageProductService.GetAllPagingProducts(request);
            return Ok(products);
        }

        //http://localhost:port/products/id
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _manageProductService.Create(request);
            if (productId == 0)
                return BadRequest();

            var product = await _manageProductService.getByProductId(productId);

            return CreatedAtAction(nameof(GetByProductId), new { id = productId }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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

        [HttpPatch("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var successful = await _manageProductService.UpdatePrice(productId, newPrice);
            if (successful)
                return Ok();
            return BadRequest();
        }

        //http://localhost:port/products/id
        [HttpGet("{productId}/images")]
        public async Task<IActionResult> GetImageById(int productId)
        {
            var imageId = await _manageProductService.GetImageById(productId);
            if (imageId == null)
            {
                return BadRequest($"Cannot find imageId:{imageId}");
            }
            return Ok(imageId);
        }

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImages(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _manageProductService.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _manageProductService.GetImageById(productId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }


        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImages(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var image = await _manageProductService.UpdateImage(imageId, request);
            if (image == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("images/{imageId}")]
        public async Task<IActionResult> DeleteImages(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _manageProductService.RemoveImage(imageId);
            if (productId == 0)
                return BadRequest();

            return Ok();
        }

        [HttpGet("images/{productId}")]
        public async Task<IActionResult> GetAllImagesById(int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var products = await _manageProductService.GetListImage(productId);
            if (products == null)
                return BadRequest();

            return Ok(products);
        }
    }
}
