using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class Player
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        public PlayerRoles? PreferredRole { get; set; }
        public bool LookingForTeam { get; set; }
        public string Tag { get; set; }

        public ApplicationUser User { get; set; }

        public Rank Rank { get; set; }

        public ICollection<TeamPlayer> Teams { get; set; }
        public ICollection<TeamOwner> OwnedTeams { get; set; }
        public ICollection<TeamInvite> Invites { get; set; }
    }
}
