using Newtonsoft.Json;

namespace EncDashboard.Models.Loan
{
    public class Fields
    {
        [JsonProperty("Loan.LoanFolder")]
        public string LoanLoanFolder { get; set; }

        [JsonProperty("Loan.LoanNumber")]
        public string LoanLoanNumber { get; set; }

        [JsonProperty("Fields.745")]
        public string Fields745 { get; set; }
    }
}
