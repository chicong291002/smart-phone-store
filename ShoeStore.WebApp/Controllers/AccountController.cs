using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShoeStore.AdminApp.ApiIntegration.Products;
using SmartPhoneStore.AdminApp.ApiIntegration.Products;
using SmartPhoneStore.AdminApp.ApiIntegration.Users;
using SmartPhoneStore.ViewModels.System.Users;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Security.Claims;

namespace SmartPhoneStore.WebApp.Controllers
{
    public class AccountController : Controller
    { 

    private readonly IUserApiClient _userApiClient;
    private readonly IOrderApiClient _orderApiClient;
    private readonly IProductApiClient _productApiClient;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountController(IUserApiClient userApiClient, IOrderApiClient orderApiClient, IProductApiClient productApiClient,
        IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _userApiClient = userApiClient;
        _orderApiClient = orderApiClient;
        _productApiClient = productApiClient;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var claims = User.Claims.ToList();
            var identifierClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            Guid id = identifierClaim != null && Guid.TryParse(identifierClaim.Value, out var parsedId)
                ? parsedId
                : Guid.Empty;
            //id = "96A8DC33-0DBF-4725-6EFB-08DBB0215135";
            var orders = await _orderApiClient.GetOrderByUser(id.ToString());
            return View(orders);
        }

        [HttpGet] 
        public async Task<IActionResult> Edit(Guid userid)
        {
            var result = await _userApiClient.GetById(userid);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UserUpdateRequest()
                {
                    Email = user.email,
                    Address = user.Address,
                    UserName = user.userName,
                    Name = user.Name,
                    PhoneNumber = user.phoneNumber,
                    Id = userid
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.Update(request.Id, request);
            if (result.IsSuccessed)
            {
                var claims = User.Claims.ToList();
                Guid id = new Guid(claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                var orders = await _orderApiClient.GetOrderByUser(id.ToString());

                TempData["UpdateAccountSuccess"] = "Cập nhật thông tin cá nhân thành công";
                return View("Index", orders);
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetail(string name, int orderId)
        {
            var order = await _orderApiClient.GetOrderById(orderId);
            order.Name = name;

            foreach (var item in order.OrderDetails)
            {
                var product = await _productApiClient.GetByProductId(item.ProductId);
                item.Name = product.Name;
                item.Price = product.Price;
                item.ThumbnailImage = product.ThumbnailImage;
            }

            return View(order);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.ChangePassword(model);

            if (result.IsSuccessed)
            {
                var claims = User.Claims.ToList();
                Guid id = new Guid(claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                var orders = await _orderApiClient.GetOrderByUser(id.ToString());

                TempData["ChangePasswordSuccess"] = "Cập nhật mật khẩu thành công";
                return View("Index", orders);
            }

            ModelState.AddModelError("", result.Message);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CancelOrderStatus(int orderId)
        {
            var result = await _orderApiClient.CancelOrderStatus(orderId);

            if (result)
            {
                TempData["CancelOrderSuccess"] = "Huỷ đơn hàng thành công";
                return RedirectToAction("Index", "Account");
            }

            //ModelState.AddModelError("", "Huỷ đơn hàng thành công");
            TempData["CancelOrderFail"] = "Huỷ đơn hàng không thành công";
            return RedirectToAction("Index", "Account");
        }
    }
}
