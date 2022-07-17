using System.ComponentModel.DataAnnotations;

namespace allu_decor_be.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
