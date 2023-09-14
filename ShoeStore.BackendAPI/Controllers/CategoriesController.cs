﻿using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Catalog.Categories;
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
        public async Task<IActionResult> GetAllCategorys()
        {
            var products = await _categoryService.GetAllCategorys();
            return Ok(products);
        }   
    }
}
