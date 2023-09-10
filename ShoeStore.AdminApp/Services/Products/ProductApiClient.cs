using Newtonsoft.Json;
using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.Common;
using ShoeStore.Application.DTOS;
using ShoeStore.Application.System.Users.DTOS;
using System.Net.Http.Headers;
using System.Net.Http;

namespace ShoeStore.AdminApp.Services.Products
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllProductsPaging(GetProductPagingRequest request)
        {
            var data = await GetAsync<PagedResult<ProductViewModel>>
                ($"/api/products/paging?pageIndex={request.PageIndex}&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}");
            return data;
        }
    }
}
