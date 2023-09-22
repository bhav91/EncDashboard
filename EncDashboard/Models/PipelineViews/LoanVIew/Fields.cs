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

        public string DaysToClosing
        {
            get
            {
                if (this.Fields745 != "")
                {
                    var difference = DateTime.Now - DateTime.Parse(this.Fields745);
                    return $"{difference.Days} days {difference.Hours} hours {difference.Minutes} minutes";
                }
                return "";
                
                
            }
            set
            {
                DaysToClosing= value;
            }
        }
    }
}
