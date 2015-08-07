using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class Match
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MatchId { get; set; }
        public Team Winner { get; set; }
        public Series Series { get; set; }
        public ICollection<Matchup> Matchups { get; set; }
    }
}
