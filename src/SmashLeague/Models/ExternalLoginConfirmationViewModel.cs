using System.ComponentModel.DataAnnotations;

namespace SmashLeague.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
