using System.ComponentModel.DataAnnotations;

namespace SmashLeague.Data
{
    public class DefaultSeason
    {
        [Key]
        public string Name { get; set; }
        [Required]
        public Season Season { get; set; }
    }
}
