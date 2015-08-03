using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class Series
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeriesId { get; set; }

        public ICollection<Match> Matches { get; set; }
        public Team Winner { get; set; }

        [Required]
        [Range(3, int.MaxValue)]
        public int MatchCount { get; set; }
    }
}
