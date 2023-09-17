using EncDashboard.Models;

namespace EncDashboard.Services
{
    public interface IAppSettingsServices
    {
        List<Persona> extractPersonas();
        List<List<PipelineView>> extractPipelineViews(List<Persona> personas);
    }
}