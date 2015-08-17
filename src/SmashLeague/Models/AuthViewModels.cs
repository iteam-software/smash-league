using SmashLeague.Security;
using System.ComponentModel.DataAnnotations;

namespace SmashLeague.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        private const string InvalidUsernameRegex = "Username must begin with a letter and can contain only letters and numbers";

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Compare("Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [RegularExpression(AuthenticationDefaults.UsernameRegex, ErrorMessage = InvalidUsernameRegex)]
        [MinLength(3)]
        [MaxLength(16)]
        public string Username { get; set; }
    }
}
