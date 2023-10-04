using SmartPhoneStore.ViewModels.Common;
using SmartPhoneStore.ViewModels.System.Roles;
using SmartPhoneStore.ViewModels.Utilities.Slides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiIntegration.Slides
{
    public interface ISlideApiClient
    {
        Task<List<SlideViewModel>> GetAllSlides();
    }
}
