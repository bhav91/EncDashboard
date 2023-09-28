using Newtonsoft.Json;

namespace EncDashboard.Models.PipelineViews
{
    public class Fields
    {
        //Fields Getting From Apiwh
        [JsonProperty("Loan.LoanFolder")]
        public string LoanFolder { get; set; }

        [JsonProperty("Fields.4000")]
        public string Fields4000 { get; set; }

        [JsonProperty("Loan.LoanNumber")]
        public string LoanNumber { get; set; }

        [JsonProperty("Loan.LoanRate")]
        public string LoanRate { get; set; }

        [JsonProperty("Loan.LoanAmount")]
        public string LoanAmount { get; set; }

        [JsonProperty("Loan.LastModified")]
        public string LastModified { get; set; }

        [JsonProperty("Loan.BorrowerName")]
        public string BorrowerName { get; set; }

        [JsonProperty("Fields.745")]
        public string Fields745 { get; set; }

        [JsonProperty("Fields.CX.SUSPENDED.DATE")]
        public string FieldsCXSUSPENDEDDATE { get; set; }

        [JsonProperty("Fields.CX.PA.EXP")]
        public string FieldsCXPAEXP { get; set; }

        [JsonProperty("Log.MS.Date.Initial UW Review")]
        public string LogMSDateInitialUWReview { get; set; }


        //Calculated fields
        public string DaysToClosing { get; set; }
        public string DaysToSuspend { get; set; }

        public string DaysSinceUW { get; set; }

        public string DaysToExpire { get; set; }

    }
}
