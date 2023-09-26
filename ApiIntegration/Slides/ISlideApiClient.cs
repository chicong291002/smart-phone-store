using ShoeStore.ViewModels.Common;
using ShoeStore.ViewModels.System.Roles;
using ShoeStore.ViewModels.Utilities.Slides;
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
