using SmashLeague.Data;

namespace SmashLeague.Services
{
    public class TeamResult : ManagerResult<TeamError>
    {
        public Team Team { get; set; }
    }
}
