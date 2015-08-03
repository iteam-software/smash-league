using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class Rank
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RankId { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Position { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int MatchMakingRating { get; set; }
    }
}
