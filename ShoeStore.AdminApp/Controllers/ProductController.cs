using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public ProductController(IProductApiClient productApiClient, IConfiguration configuration
            , ICategoryApiClient categoryApiClient)
        {
            _configuration = configuration;
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            Console.WriteLine(categoryId);
            var request = new GetProductPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                CategoryIds = categoryId
            };
            var data = await _productApiClient.GetAllProductsPaging(request);
            ViewBag.keyword = keyword;

            var categories = await _categoryApiClient.GetAllCategorys();

            ViewBag.categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = categoryId.HasValue && categoryId.Value == x.Id
            });

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
            ModelState.AddModelError("", "Thêm Sản Phẩm thất bại");  //lỗi model bussiness
            //message tu api truyen vao 
            return View(request); // ko co thi tra ve view voi du~ lieu co san
        }
    }
}
