using SmashLeague.Data;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public interface IPlayerManager
    {
        Task<Player> CreatePlayerForUserAsync(ApplicationUser user);
        Task<Player> GetPlayerByUserNameAsync(string username);
        Task<Player[]> GetPlayersByPartialNameAsync(string partial);
        Task<Player[]> GetPlayersAsync(int? max = null);
    }
}
