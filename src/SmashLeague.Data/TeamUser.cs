using System.Collections.Generic;

namespace SmashLeague.Data
{
    public class TeamUser : ApplicationUser
    {
        public ICollection<Team> Teams { get; set; }
    }
}
