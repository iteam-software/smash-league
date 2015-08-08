using System.ComponentModel.DataAnnotations;

namespace SmashLeague.Models
{
    public class TeamViewModel
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
