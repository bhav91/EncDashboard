using System.ComponentModel.DataAnnotations;

namespace EncDashboard.Models.auth
{
    public class Login
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}
