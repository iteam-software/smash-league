using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class RankBracket
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RankingBracketId { get; set; }

        [Required]
        public string Name { get; set; }
        public RankBrackets Type { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int MinimumMMR { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int MaximumMMR { get; set; }

        [Required]
        public Season Season { get; set; }

        public ICollection<Rank> Rankings { get; set; }
    }
}
