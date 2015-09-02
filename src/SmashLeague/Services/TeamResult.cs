using SmashLeague.Data;
using System;

namespace SmashLeague.Services
{
    public class TeamResult : ManagerResult<TeamError>
    {
        public Team Team { get; set; }

        public static TeamResult Failed(Exception e)
        {
            var result = new TeamResult { Succeeded = false };
            result.Errors = new TeamError[] { new TeamError(e) };

            return result;
        }

        public static TeamResult Success(Team team)
        {
            return new TeamResult { Team = team, Succeeded = true };
        }
    }
}
