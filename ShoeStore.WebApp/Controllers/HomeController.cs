using ApiIntegration.Slides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoeStore.WebApp.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ShoeStore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISlideApiClient _slideApiClient;

        public HomeController(ILogger<HomeController> logger,ISlideApiClient slideApiClient)
        {
            _logger = logger;
            _slideApiClient = slideApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var slides = await _slideApiClient.GetAllSlides();
            var viewModel = new HomeViewModel
            {
                Slides = slides
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