using Newtonsoft.Json;
using ShoeStore.Application.System.Users.DTOS;
using System.Text;

namespace ShoeStore.AdminApp.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json,Encoding.UTF8,"application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7204");
            var response = await client.PostAsync("/api/user/authenticate", httpContent) ;
            var token = await response.Content.ReadAsStringAsync();

            return token;

        }

        public Task<bool> Rigister(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
