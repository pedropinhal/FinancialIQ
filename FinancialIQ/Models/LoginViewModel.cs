using System.ComponentModel.DataAnnotations;

namespace FinancialIQ.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

    }
}