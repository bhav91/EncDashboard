using EncDashboard.Models;
using EncDashboard.Models.auth;

namespace EncDashboard.Services.AppSettingServices
{
    public interface IAppSettingsServices
    {
        List<Persona> extractPersonas();
    }
}