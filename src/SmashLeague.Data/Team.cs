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

        [Required]
        public TeamOwner Owner { get; set; }

        public ICollection<Rank> Rankings { get; set; }
        public ICollection<TeamPlayer> Members { get; set; }
        public ICollection<Matchup> Matchups { get; set; }
        public ICollection<Match> Wins { get; set; }
        public ICollection<TeamInvite> Invitees { get; set; }
    }
}
