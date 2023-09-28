using EncDashboard.Models.PipelineViews;

namespace EncDashboard.Models.Others
{
    public class LoanViewModel
    {
        public List<Persona> Personas { get; set; }  
        public List<string> Columns { get; set; }

        public List<LoanRecords> LoanRecords { get; set; }
    }
}
