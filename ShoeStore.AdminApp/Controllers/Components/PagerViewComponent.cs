using Microsoft.AspNetCore.Mvc;
using SmartPhoneStore.Application.Common;
using SmartPhoneStore.ViewModels.Common;

namespace SmartPhoneStore.AdminApp.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PageResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
