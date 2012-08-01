using System.ComponentModel.DataAnnotations;

namespace FinancialIQ.Models
{
    public class RegisterViewModel 
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        public string Email { get; set; }
    }
}