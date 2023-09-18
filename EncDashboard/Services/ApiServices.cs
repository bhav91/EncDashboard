using EncDashboard.Models.auth;
using Microsoft.Kiota.Abstractions;
using Newtonsoft.Json;

namespace EncDashboard.Services
{
    public class ApiServices : IApiServices
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ApiServices(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<Token> getToken()
        {
            var _apiConfig = _configuration.GetSection("APIConfig").Get<APIConfig>();
            try
            {
                var requestContent = new FormUrlEncodedContent(new[] {
                     new KeyValuePair<string, string>("grant_type", "password"),
                     new KeyValuePair<string, string>("username", _apiConfig.username),
                     new KeyValuePair<string, string>("password", _apiConfig.password),
                     new KeyValuePair<string, string>("client_id", _apiConfig.client_id),
                     new KeyValuePair<string, string>("client_secret", _apiConfig.client_secret),
                 });

                _httpClient.BaseAddress = new Uri(_apiConfig.baseURL);
                var response = await _httpClient.PostAsync("/oauth2/v1/token", requestContent);
                if(response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var token=JsonConvert.DeserializeObject<Token>(stringResponse);
                    if (token != null)
                    {
                        return token;
                    }
                }
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return null;
        }


    }
}
