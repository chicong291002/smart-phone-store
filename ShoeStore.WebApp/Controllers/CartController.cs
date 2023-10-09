using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartPhoneStore.AdminApp.ApiIntegration.Products;
using SmartPhoneStore.Utilities.Constants;
using SmartPhoneStore.ViewModels.Sales;
using SmartPhoneStore.WebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartPhoneStore.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductApiClient _productApiClient;

        public CartController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Authorize]
        public IActionResult Checkout()
        {
            return View(GetCheckoutViewModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout(CheckoutRequest request)
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetListItems()
        {
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);
            var currentCart = new CartViewModel();
            currentCart.CartItems = new List<CartItemViewModel>();

           // List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
            {
                currentCart = JsonConvert.DeserializeObject<CartViewModel>(session);
            }
            return Ok(currentCart);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _productApiClient.GetByProductId(id);
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);

            // List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            var currentCart = new CartViewModel();
            currentCart.CartItems = new List<CartItemViewModel>();
            if (session != null)
            {
                //currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
                currentCart = JsonConvert.DeserializeObject<CartViewModel>(session);
            }

            int quantity = 1;
            if (currentCart.CartItems.Any(x => x.ProductId == id))
            {
                if (currentCart.CartItems.First(x => x.ProductId == id).Quantity == product.Stock)
                {
                    return Ok(currentCart.CartItems);
                }

                quantity = currentCart.CartItems.First(x => x.ProductId == id).Quantity + quantity;
                currentCart.CartItems.First(x => x.ProductId == id).Quantity = quantity;
            }
            else
            {

                var cartItem = new CartItemViewModel()
                {
                    ProductId = id,
                    Image = product.ThumbnailImage,
                    Name = product.Name,
                    Quantity = quantity,
                    Price = product.Price,
                };
                currentCart.CartItems.Add(cartItem);
            }
            HttpContext.Session.SetString(SystemConstants.CartSession, JsonConvert.SerializeObject(currentCart));
            return Ok(currentCart);
        }

        public async Task<IActionResult> UpdateCart(int id, int quantity)
        {
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);

            var currentCart = new CartViewModel();
            currentCart.CartItems = new List<CartItemViewModel>();

            if (session != null)
            {
                //currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
                currentCart = JsonConvert.DeserializeObject<CartViewModel>(session);
            }

            foreach (var item in currentCart.CartItems)
            {
                if (item.ProductId == id)
                {
                    var product = await _productApiClient.GetByProductId(item.ProductId);
                    var productStock = product.Stock;

                    if (quantity == 0 && currentCart.CartItems.Count >= 1)
                    {
                        currentCart.CartItems.Remove(item); 
                        break;
                    }
                   /* else if (quantity == 0 && currentCart.CartItems.Count == 1) // for what ?
                    {
                        currentCart.CartItems.Remove(item);
                        //currentCart.Promotion = 0;
                        break;
                    }*/
                    else if (quantity > productStock)
                    {
                        return Content("quantity is greater than stock");
                    }
                    item.Quantity = quantity;
                }
            }

            HttpContext.Session.SetString(SystemConstants.CartSession, JsonConvert.SerializeObject(currentCart));

            return Ok(currentCart);
        }

        private CheckoutViewModel GetCheckoutViewModel()
        {
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);

            //var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            var claims = User.Claims.ToList();

            var name = claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value;
            var email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            //var address = claims.FirstOrDefault(x => x.Type == ClaimTypes.StreetAddress).Value;
            //var phoneNumber = claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone).Value;

            var currentCart = new CartViewModel();
            currentCart.CartItems = new List<CartItemViewModel>();

            if (session != null)
            {
                //currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
                currentCart = JsonConvert.DeserializeObject<CartViewModel>(session);
            }

            var checkoutVm = new CheckoutViewModel()
            {
                CartItems = currentCart.CartItems,
                CheckoutModel = new CheckoutRequest(),
                Name = name.ToString(),
               // Address = address.ToString(),
                //PhoneNumber = phoneNumber.ToString(),
                //Promotion = currentCart.Promotion,
                //CouponCode = currentCart.CouponCode
            };

            return checkoutVm;
        }
    }
}
