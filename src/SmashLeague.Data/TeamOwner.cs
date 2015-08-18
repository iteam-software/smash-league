using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class TeamOwner
    {
        public int TeamId { get; set; }
        public int PlayerId { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
    }
}
