namespace EncDashboard.Models.auth
{
    public class APIConfig
    {
        public string baseURL { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string instance { get; set; }
        public int CacheExpiryMin { get; set; }
    }
}
