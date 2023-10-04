using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SmartPhoneStore.AdminApp.ApiIntegration;
using SmartPhoneStore.Data.Entities;
using SmartPhoneStore.ViewModels.Utilities.Slides;

namespace ApiIntegration.Slides
{
    public class SlideApiClient : BaseApiClient, ISlideApiClient
    {
        public SlideApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<List<SlideViewModel>> GetAllSlides()
        {
            return await GetListAsync<SlideViewModel>("/api/slides");
        }
    }
}
