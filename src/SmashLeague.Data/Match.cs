using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class Match
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MatchId { get; set; }

        [Required]
        public Team Team1 { get; set; }

        [Required]
        public Team Team2 { get; set; }

        public Team Winner { get; set; }

        public Series Series { get; set; }
    }
}
