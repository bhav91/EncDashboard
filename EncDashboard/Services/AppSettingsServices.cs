using EncDashboard.Models;
using Newtonsoft.Json;

namespace EncDashboard.Services
{
    public class AppSettingsServices : IAppSettingsServices
    {
        private readonly List<Persona> _matchingPersonas = new List<Persona>();
        private readonly List<List<PipelineView>> _pipelineViews = new List<List<PipelineView>>();

        public List<Persona> extractPersonas()
        {
            string json = System.IO.File.ReadAllText("appsettings.json");
            PersonasConfig config = JsonConvert.DeserializeObject<PersonasConfig>(json);
            config.UserPersonas.ForEach(userPersona =>
            {
                var matchingPersona = config.Personas.Find(p => p.name == userPersona);
                if (matchingPersona != null)
                {
                    if (_matchingPersonas != null)
                    {
                        _matchingPersonas.Add(matchingPersona);
                    }

                }

            });
            return _matchingPersonas;
        }
        public List<List<PipelineView>> extractPipelineViews(List<Persona> personas)
        {
            foreach (var persona in personas)
            {
                _pipelineViews.Add(persona.pipelineViews);
            }



            return _pipelineViews;
        }
    }

}
