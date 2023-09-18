using EncDashboard.Models;
using EncDashboard.Models.auth;

namespace EncDashboard.Services
{
    public interface IAppSettingsServices
    {
        List<Persona> extractPersonas();
        List<List<PipelineView>> extractPipelineViews(List<Persona> personas);

        void saveToken(Token token);
    }
}