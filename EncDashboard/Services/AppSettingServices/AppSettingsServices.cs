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
        private readonly ICacheService _cacheService;

        public AppSettingsServices(ICacheService cacheService)
        {
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
                    return _matchingPersonas;
                }
                
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.ToString());
            }
            return _matchingPersonas;
            
        }

    }

}
