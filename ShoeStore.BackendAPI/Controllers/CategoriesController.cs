using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPhoneStore.Application.Catalog.Categories;
using SmartPhoneStore.ViewModels.Catalog.Categories;
using SmartPhoneStore.ViewModels.Catalog.Products;
using System.Threading.Tasks;

namespace ShoeStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllCategorysPagings([FromQuery] GetProductPagingRequest request)
        {
            var categorys = await _categoryService.GetAllCategoryPaging(request);
            return Ok(categorys);
        }

        //http://localhost:port/categorys/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBycategoryId(int id)
        {
            var category = await _categoryService.getByCategoryId(id);
            return Ok(category);
        }

        [HttpPost]
        [Authorize]
        //chap nhan doi tuong form len 
        public async Task<IActionResult> Create([FromBody] CategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryId = await _categoryService.Create(request);
            if (categoryId == 0)
                return BadRequest();

            var category = await _categoryService.getByCategoryId(categoryId);

            return CreatedAtAction(nameof(GetBycategoryId), new { id = categoryId }, category);
        }

        [HttpPut("updateCategory")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }
            var category = await _categoryService.Update(request);
            if (category == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var categoryId = await _categoryService.Delete(id);
            if (categoryId == 0)
                return BadRequest();

            return Ok();
        }
    }
}
