using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartPhoneStore.AdminApp.ApiIntegration.Products;
using SmartPhoneStore.Utilities.Constants;
using SmartPhoneStore.WebApp.Models;
using System.Collections.Generic;
using System.Linq;
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
                    Description = product.Description,
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
    }
}
