using SmashLeague.Data;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public interface IRankManager
    {
        Task CreateNewTeamRankingAsync(Team team, Season season = null);
    }
}
