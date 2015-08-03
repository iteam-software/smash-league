using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmashLeague.Data
{
    public class RankingBracket
    {
        [Key]
        public string Name { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int MinimumMMR { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int MaximumMMR { get; set; }

        public ICollection<Rank> Rankings { get; set; }
    }
}
