namespace EncDashboard.Models.UserDetails
{
    public class UserDetails
    {
        
            public string id { get; set; }
            public string lastName { get; set; }
            public string firstName { get; set; }
            public string fullName { get; set; }
            public string email { get; set; }
            public string workingFolder { get; set; }
            public string subordinateLoanAccess { get; set; }
            public List<string> userIndicators { get; set; }
            public string peerLoanAccess { get; set; }
            public DateTime lastLogin { get; set; }
            public bool personalStatusOnline { get; set; }
            public string comments { get; set; }
            public List<UserPersona> personas { get; set; }
            public CcSite ccSite { get; set; }
       
    }
}
