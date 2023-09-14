using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Common;
using ShoeStore.ViewModels.Common;

namespace ShoeStore.AdminApp.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PageResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
