using Microsoft.AspNetCore.Mvc;
using SmartPhoneStore.AdminApp.ApiIntegration.Categories;
using System.Threading.Tasks;

namespace SmartPhoneStore.WebApp.Controllers.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;

        public NavigationViewComponent(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryList = await _categoryApiClient.GetAllCategorys();

            return View("Default", categoryList);
        }
    }
}
