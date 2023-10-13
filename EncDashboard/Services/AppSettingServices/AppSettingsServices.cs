using EncDashboard.Models;
using EncDashboard.Models.auth;
using EncDashboard.Models.UserDetails;
using EncDashboard.Services.Cached_Service;
using Newtonsoft.Json;

namespace EncDashboard.Services.AppSettingServices
{
    public class AppSettingsServices : IAppSettingsServices
    {
        private readonly List<Persona> _matchingPersonas = new List<Persona>();
        private readonly ILogger<AppSettingsServices> _logger;
        private readonly ICacheService _cacheService;

        public AppSettingsServices(ILogger<AppSettingsServices> logger,ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }
        public List<Persona> extractPersonas()
        {
            try
            {
                string appsettingsJson = File.ReadAllText("appsettings.json");
                PersonasConfig config = JsonConvert.DeserializeObject<PersonasConfig>(appsettingsJson);
                var userDetails = _cacheService.Get<UserDetails>("userDetails");
                if(userDetails != null) 
                {
                    foreach (var persona in userDetails.personas)
                    {
                        var matchingPersona = config.Personas.Find(p => p.name == persona.entityName);
                        if (matchingPersona != null)
                        {
                            _matchingPersonas.Add(matchingPersona);

                        }
                    }
                    _logger.LogInformation("Matching Pesonas Found");
                    return _matchingPersonas;
                }
                
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.ToString());
            }
            _logger.LogWarning("No Matching Personas");
            return _matchingPersonas;
            
        }

    }

}
