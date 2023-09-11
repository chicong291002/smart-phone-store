using Microsoft.AspNetCore.Mvc;

namespace ShoeStore.AdminApp.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
