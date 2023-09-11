using Azure.Core;
using ShoeStore.Application.Catalog.Categories.DTOS;
using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.Common;
using ShoeStore.Application.DTOS;

namespace ShoeStore.AdminApp.Services.Categories
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<CategoryViewModel>> GetAllCategorys()
        {
            var data = await GetListAsync<CategoryViewModel>
               ($"/api/categories");
            return data;
        }
    }
}
