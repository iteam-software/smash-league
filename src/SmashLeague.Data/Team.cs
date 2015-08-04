using Microsoft.Data.Entity.Metadata;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class Team
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Rank> Rankings { get; set; }
        public ICollection<Player> Members { get; set; }
        public ICollection<Match> Matches { get; set; }
    }
}
