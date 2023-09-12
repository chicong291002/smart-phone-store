using Microsoft.AspNetCore.Mvc;
using ShoeStore.AdminApp.Services.Categories;
using ShoeStore.AdminApp.Services.Products;
using ShoeStore.AdminApp.Services.Roles;
using ShoeStore.AdminApp.Services.Users;
using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.Common;
using ShoeStore.Application.System.Users.DTOS;

namespace ShoeStore.AdminApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;
        private readonly ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient productApiClient, IConfiguration configuration,ICategoryApiClient categoryApiClient)
        {
            _configuration = configuration;
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetProductPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _productApiClient.GetAllProductsPaging(request);
            ViewBag.keyword = keyword;

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }

            return View(data); // ra duoc pageUser
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _productApiClient.CreateProduct(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("","Thêm Sản Phẩm thất bại");  //lỗi model bussiness
            //message tu api truyen vao 
            return View(request); // ko co thi tra ve view voi du~ lieu co san
        }

        [HttpGet("{id}/CategoryAssign")]
        public async Task<IActionResult> CategoryAssign(int id)
        {
            var roleAssinRequest = await GetCategoryRoleAssignRequest(id);
            return View(roleAssinRequest);
        }

        [HttpPost("CategoryAssign")]
        public async Task<IActionResult> CategoryAssign(CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(); //lỗi validation modelstate trả về 
            }

            var result = await _productApiClient.CategoryAssign(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật danh mục thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            var roleAssinRequest = await GetCategoryRoleAssignRequest(request.Id);
            return View(roleAssinRequest); // ko co thi tra ve view voi du~ lieu co san
        }

        private async Task<CategoryAssignRequest> GetCategoryRoleAssignRequest(int id)
        {
            var productObj = await _productApiClient.GetByProductId(id);
            var categoriesObj = await _categoryApiClient.GetAllCategorys(); //danh sach Roles    

            var categoryAssinRequest = new CategoryAssignRequest();

            foreach (var role in categoriesObj)
            {
                categoryAssinRequest.Categories.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = productObj.Categories.Contains(role.Name),
                });
            }
            return categoryAssinRequest;
        }
    }
}
