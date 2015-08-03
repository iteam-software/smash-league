using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class RankedTeam : Team
    {
        public int RankId { get; set; }

        [Required, ForeignKey("RankId")]
        public Rank Rank { get; set; }
    }
}
