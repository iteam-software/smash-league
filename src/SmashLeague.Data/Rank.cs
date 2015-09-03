using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class Rank
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RankId { get; set; }

        [Required]
        public RankBracket RankBracket { get; set; }

        [Required]
        public Rating Rating { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Position { get; set; }
    }
}
