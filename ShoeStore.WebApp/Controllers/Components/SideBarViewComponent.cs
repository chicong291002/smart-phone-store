using Microsoft.AspNetCore.Mvc;
using SmartPhoneStore.AdminApp.ApiIntegration.Categories;
using System.Globalization;
using System.Threading.Tasks;

namespace ShoeStore.WebApp.Controllers.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;

        public SideBarViewComponent(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _categoryApiClient.GetAllCategorys();
            return View(items);
        }
    }
}
