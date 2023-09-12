using ShoeStore.Application.Catalog.Categories.DTOS;

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

        public async Task<CategoryViewModel> GetById(int id)
        {
            var data = await GetAsync<CategoryViewModel>($"/api/categories/{id}");
            return data;
        }
    }
}
