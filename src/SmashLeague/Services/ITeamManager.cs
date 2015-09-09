using SmashLeague.Data;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public interface ITeamManager
    {
        Task<Player[]> SuggestAsync(DataTransferObjects.Player[] players);
        Task<TeamResult> CreateTeamAsync(DataTransferObjects.Team team);
        Task<Team[]> GetTeamsForPlayerAsync(string username);
        Task<Team[]> GetTopTeamsAsync(int number);
        Task<Team[]> SearchForTeamsAsync(string q);
        Task<Team> FindTeamByNormalizedNameAsync(string normalizedName);
        Task<Team> UpdateTeamOwner(Team team, string newOwner);
        Task<Team> UpdateTeam(DataTransferObjects.Team team);
    }
}
