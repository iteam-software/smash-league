
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class Matchup
    {
        public int MatchId { get; set; }
        [ForeignKey("MatchId"), Required]
        public Match Match { get; set; }

        public int TeamId { get; set; }
        [ForeignKey("TeamId"), Required]
        public Team Team { get; set; }
    }
}
