using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class Rating
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingId { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int MatchMakingRating { get; set; }
    }
}