using EncDashboard.Models.auth;

namespace EncDashboard.Services
{
    public interface IApiServices
    {
        Task<Token> getToken();
    }
}