using EncDashboard.Models.auth;
using EncDashboard.Models.Loan;
using EncDashboard.Models.UserDetails;
using System.Threading.Tasks;

namespace EncDashboard.Services.ApiServices
{
    public interface IApiServices
    {
        Task<Token?> getToken();
        Task<UserDetails?> getPersonas();
        Task<List<LoanRecords>?> getLoanRecords();
    }
}