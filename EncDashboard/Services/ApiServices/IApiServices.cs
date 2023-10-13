using EncDashboard.Models.auth;
using EncDashboard.Models.PipelineViews;
using EncDashboard.Models.UserDetails;
using System.Threading.Tasks;

namespace EncDashboard.Services.ApiServices
{
    public interface IApiServices
    {
        Task<Token?> getToken();
        Task<UserDetails?> getUserDetails();
        Task<List<LoanRecords>?> getLoanRecords(List<string> columns,string view);
    }
}