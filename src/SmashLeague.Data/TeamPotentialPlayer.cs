using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class TeamPotentialPlayer
    {
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public Team Team { get; set; }

        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
    }
}
