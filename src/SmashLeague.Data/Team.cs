using Microsoft.Data.Entity.Metadata;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SmashLeague.Data
{
    public class Team
    {
        public readonly static Regex TeamNameRegex = new Regex("^[a-zA-Z -]{3,50}$");

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string NormalizedName { get; set; }

        [Required]
        public TeamOwner Owner { get; set; }

        public ICollection<Rank> Rankings { get; set; }
        public ICollection<TeamPlayer> Members { get; set; }
        public ICollection<Matchup> Matchups { get; set; }
        public ICollection<Match> Wins { get; set; }
        public ICollection<TeamInvite> Invitees { get; set; }
    }
}
