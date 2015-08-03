using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class Season
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeasonId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Match> Matches { get; set; }
        public ICollection<Series> Series { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<RankingBracket> Brackets { get; set; }
    }
}
