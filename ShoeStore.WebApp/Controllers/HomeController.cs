using ApiIntegration.Slides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartPhoneStore.AdminApp.ApiIntegration.Products;
using SmartPhoneStore.Utilities.Constants;
using SmartPhoneStore.WebApp.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SmartPhoneStore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISlideApiClient _slideApiClient;
        private readonly IProductApiClient _productApiClient;

        public HomeController(ILogger<HomeController> logger,ISlideApiClient slideApiClient,IProductApiClient productApiClient)
        {
            _logger = logger;
            _slideApiClient = slideApiClient;
            _productApiClient = productApiClient;
        }

        public async Task<IActionResult> Index()
        {
            //var slides = await _slideApiClient.GetAllSlides();
            var viewModel = new HomeViewModel
            {
                //Slides = slides,
                FeaturedProducts = await _productApiClient.GetFeaturedProducts(SystemConstants.ProductSettings.NumberOfFeaturedProducts),
                LatestProducts = await _productApiClient.GetLatestProducts(SystemConstants.ProductSettings.NumberOfLatestProducts)
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}