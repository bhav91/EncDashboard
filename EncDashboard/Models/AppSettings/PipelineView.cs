

namespace EncDashboard.Models
{
    public class PipelineView
    {
        public string name { get; set; }
        public List<string> resultFields { get; set; }
        public List<CalcField> calcFields { get; set; }
    }
}
