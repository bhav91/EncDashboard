namespace EncDashboard.Services.CalculationService
{
    public class CalculateService : ICalculateService
    {
        public string calculateDays(string op)
        {
            if(op == "")
            {
                return "";
            }
            return DateTime.Parse(op).Subtract(DateTime.Now).Days.ToString();
        }
    }
}
