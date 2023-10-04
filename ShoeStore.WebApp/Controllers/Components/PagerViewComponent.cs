using Microsoft.AspNetCore.Mvc;
using SmartPhoneStore.ViewModels.Common;
using System.Threading.Tasks;

namespace SmartPhoneStore.WebApp.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PageResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
