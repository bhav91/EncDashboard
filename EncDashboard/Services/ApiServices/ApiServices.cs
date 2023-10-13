
using EncDashboard.Models;
using EncDashboard.Models.AppSettings;
using EncDashboard.Models.auth;
using EncDashboard.Models.PipelineViews;
using EncDashboard.Models.UserDetails;
using EncDashboard.Services.Cached_Service;
using EncDashboard.Services.CalculationService;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace EncDashboard.Services.ApiServices
{
    public class ApiServices : IApiServices
    {
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;
        private readonly ICalculateService _calculateService;
        private readonly ILogger<ApiServices> _logger;
        private readonly HttpClient _httpClient;
        private readonly APIConfig _apiConfig;
        private readonly List<CalcField> _calcFields;

        public ApiServices(ILogger<ApiServices> logger,HttpClient httpClient, IConfiguration configuration, ICacheService cacheService,ICalculateService calculateService)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
            _cacheService = cacheService;
            _calculateService = calculateService;
            _apiConfig = _configuration.GetSection("APIConfig").Get<APIConfig>();
            _calcFields=_configuration.GetSection("CalcFields").Get<List<CalcField>>();
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
                        _logger.LogInformation("Token Saved In Cache");
                        return token;
                    }
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.ToString());
            }
            _logger.LogWarning("Token Not Found");
            return null;
        }

        public async Task<UserDetails?> getUserDetails()
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
                        userDetails.personas.Add(new UserPersona {entityName = "Under Writer" });
                        _cacheService.Remove("userDetails");
                        _cacheService.SetKey("userDetails", userDetails);
                        _logger.LogInformation("User Details Saved In Cache");
                        return userDetails;
                    }
                }
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.ToString());
            }

            _logger.LogWarning("User Details Not Found");
            return null;
        }

        public async Task<List<LoanRecords>?> getLoanRecords(List<string> columns,string view)
        {
            try
            {
                var token = _cacheService.Get<Token>("serviceToken");
                var loanJson = File.ReadAllText(String.Format("{0}.json",view.Replace(" ","")));
                var content = new StringContent(loanJson, Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.token_type, token.access_token);
                
                var response = await _httpClient.PostAsync("/encompass/v3/loanPipeline", content);
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var loanRecords = JsonConvert.DeserializeObject<List<LoanRecords>>(stringResponse);
                    
                    if(loanRecords!= null)
                    {
                        foreach(var record in loanRecords)
                        {
                            var recordFieldsType = record.fields.GetType();
                            var recordProperties = recordFieldsType.GetProperties();

                            foreach (var field in recordProperties)
                            {
                                foreach (var calc in _calcFields.Where(calc => calc.field.Contains(field.Name)))
                                {
                                    foreach (var col in columns.Where(col => col.Contains(field.Name)))
                                    {
                                        var calculationString = calc.calc.Split('-');
                                        var op1 = calculationString[0];
                                        var op2 = calculationString[1];

                                        var opField = recordFieldsType.GetProperty(op1 == "Today " ? op2.Split(":")[0].Trim() : op1.Split(":")[0])
                                            .GetValue(record.fields);
                                        var opValue = opField.ToString();

                                        var calcValue = _calculateService.calculateDays(opValue);
                                        field.SetValue(record.fields, calcValue);
                                    }
                                }
                            }
                        }
                        return loanRecords;
                    }
                }
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.ToString());
                
            }
            _logger.LogWarning("Records Not Found");
            return null;
        }

    }
}
