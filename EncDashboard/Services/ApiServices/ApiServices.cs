
using EncDashboard.Models.auth;
using EncDashboard.Models.Loan;
using EncDashboard.Models.UserDetails;
using EncDashboard.Services.Cached_Service;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EncDashboard.Services.ApiServices
{
    public class ApiServices : IApiServices
    {
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;
        private readonly HttpClient _httpClient;
        private readonly APIConfig _apiConfig;

        public ApiServices(HttpClient httpClient, IConfiguration configuration,ICacheService cacheService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _cacheService = cacheService;
            _apiConfig = _configuration.GetSection("APIConfig").Get<APIConfig>();
            _httpClient.BaseAddress = new Uri(_apiConfig.baseURL);
        }

        public async Task<Token?> getToken()
        {

            try
            {
                var requestContent = new FormUrlEncodedContent(new[] {
                     new KeyValuePair<string, string>("grant_type", "password"),
                     new KeyValuePair<string, string>("username", _apiConfig.username),
                     new KeyValuePair<string, string>("password", _apiConfig.password),
                     new KeyValuePair<string, string>("client_id", _apiConfig.client_id),
                     new KeyValuePair<string, string>("client_secret", _apiConfig.client_secret),
                 });

                var response = await _httpClient.PostAsync("/oauth2/v1/token", requestContent);
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<Token>(stringResponse);
                    if (token != null)
                    {
                        _cacheService.Remove("serviceToken");
                        _cacheService.SetKey("serviceToken", token);
                        return token;
                    }
                }
                

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return null;
        }

        public async Task<UserDetails?> getPersonas()
        {
            try
            {
                var token=_cacheService.Get<Token>("serviceToken");
                var username = _apiConfig.username.Split('@')[0];
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.token_type, token.access_token);
                var response = await _httpClient.GetAsync(string.Format("/encompass/v1/company/users/{0}", username));
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var userDetails = JsonConvert.DeserializeObject<UserDetails>(stringResponse);
                    if (userDetails != null)
                    {
                        _cacheService.Remove("userDetails");
                        _cacheService.SetKey("userDetails", userDetails);
                        return userDetails;
                    }
                }
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return null;
        }

        public async Task<List<LoanViewRecords>?> getLoanRecords()
        {
            try
            {
                var token = _cacheService.Get<Token>("serviceToken");
                var loanJson = File.ReadAllText("loan.json");
                var content = new StringContent(loanJson, Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.token_type, token.access_token);

                var response = await _httpClient.PostAsync("/encompass/v3/loanPipeline", content);
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var loanRecords = JsonConvert.DeserializeObject<List<LoanViewRecords>>(stringResponse);
                    if(loanRecords!= null)
                    {
                        
                        return loanRecords;
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
