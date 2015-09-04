using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class RankBracket
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public RankBrackets Type { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int MinimumMMR { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int MaximumMMR { get; set; }

        public int SeasonId { get; set; }
        [Required, ForeignKey("SeasonId")]
        public Season Season { get; set; }

        public ICollection<Rank> Rankings { get; set; }
    }
}
