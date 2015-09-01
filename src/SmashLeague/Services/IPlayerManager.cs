using SmashLeague.Data;
using SmashLeague.Models;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public interface IPlayerManager
    {
        Task<Player> CreatePlayerForUserAsync(ApplicationUser user);
        Task<Player> GetPlayerByUserNameAsync(string username);
        Task<Player[]> GetPlayersByPartialNameAsync(string partial);
        Task<Player[]> GetPlayersAsync(int? max = null);
        Task<Player> UpdatePlayerAsync(Profile profile);
    }
}
