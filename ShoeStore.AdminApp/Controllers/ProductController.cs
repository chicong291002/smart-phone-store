using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartPhoneStore.AdminApp.ApiIntegration.Categories;
using SmartPhoneStore.AdminApp.ApiIntegration.Products;
using SmartPhoneStore.ViewModels.Catalog.Products;

namespace SmartPhoneStore.AdminApp.Controllers
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

        public async Task<IActionResult> Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetProductPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                CategoryId = categoryId
            };

            var data = await _productApiClient.GetAllProductsPaging(request);
            ViewBag.keyword = keyword;


            var categories = await _categoryApiClient.GetAllCategorys();
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = categoryId.HasValue && categoryId.Value == x.Id
            });

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }

            return View(data); 
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryApiClient.GetAllCategorys();
            var productVM = new ProductCreateRequest()
            {
                Categories = categories
            };
            return View(productVM);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                request.CategoryId = 0;
                request.Categories = await _categoryApiClient.GetAllCategorys();
                return View(request);
            }

            var result = await _productApiClient.CreateProduct(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Thêm Sản Phẩm thất bại");
            request.CategoryId = 0;
            request.Categories = await _categoryApiClient.GetAllCategorys();
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var categories = await _categoryApiClient.GetAllCategorys();
            var product = await _productApiClient.GetByProductId(id);
            var ProductUpdateRequest = new ProductUpdateRequest()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Categories = categories,
                Price = product.Price,
                Stock = product.Stock,
            };
            return View(ProductUpdateRequest);
        }


        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                request.CategoryId = 0;
                request.Categories = await _categoryApiClient.GetAllCategorys();
                return View(request);
            }

            var result = await _productApiClient.Update(request);
            if (result)
            {
                TempData["result"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            request.CategoryId = 0;
            request.Categories = await _categoryApiClient.GetAllCategorys();
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productApiClient.GetByProductId(id);
            var category = await _categoryApiClient.GetById(product.CategoryId);

            var detailVm = new ProductViewModel()
            {
                Price = product.Price,
                Stock = product.Stock,
                Name = product.Name,
                Category = category,
                Description = product.Description,
                ThumbnailImage = product.ThumbnailImage,
                ProductImage = product.ProductImage
            };

            return View(detailVm);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new ProductDeleteRequest()
            {
                Id = id
            }); ;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductDeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _productApiClient.Delete(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Xóa sản phẩm thất bại");  
            return View(result);
        }
    }
}
