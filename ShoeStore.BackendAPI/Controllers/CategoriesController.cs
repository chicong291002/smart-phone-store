using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.Catalog.Products;
using ShoeStore.Application.Catalog.Categories;

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
        public async Task<IActionResult> GetAllCategorys()
        {
            var products = await _categoryService.GetAllCategorys();
            return Ok(products);
        }   
    }
}
