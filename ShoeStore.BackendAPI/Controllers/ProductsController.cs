using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Catalog.Products;
using ShoeStore.ViewModels.Catalog.ProductImages;
using ShoeStore.ViewModels.Catalog.Products;
using System.Threading.Tasks;

namespace ShoeStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //bat buoc phai login moi vao dc  
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

        [HttpPost]
        [Consumes("multipart/form-data")]
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
        public async Task<IActionResult> Delete(int id)
        {
            var productId = await _productService.Delete(id);
            if (productId == 0)
                return BadRequest();

            return Ok();
        }

        [HttpPatch("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var successful = await _productService.UpdatePrice(productId, newPrice);
            if (successful)
                return Ok();
            return BadRequest();
        }

        //http://localhost:port/products/id
        [HttpGet("{productId}/images")]
        public async Task<IActionResult> GetImageById(int productId)
        {
            var imageId = await _productService.GetImageById(productId);
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
            var imageId = await _productService.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _productService.GetImageById(productId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }


        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImages(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var image = await _productService.UpdateImage(imageId, request);
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
            var productId = await _productService.RemoveImage(imageId);
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
            var products = await _productService.GetListImage(productId);
            if (products == null)
                return BadRequest();

            return Ok(products);
        }

        [HttpPut("{id}/categories")]
        [Authorize]
        public async Task<IActionResult> RoleAssign(int id, [FromBody] CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.CategoryAssign(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
