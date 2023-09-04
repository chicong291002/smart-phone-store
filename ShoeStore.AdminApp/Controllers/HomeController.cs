using Microsoft.AspNetCore.Mvc;
using ShoeStore.AdminApp.Models;
using System.Diagnostics;

namespace ShoeStore.AdminApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        } 
    }
}