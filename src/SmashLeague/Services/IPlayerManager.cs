using SmashLeague.Data;
using SmashLeague.DataTransferObjects;
using System.Threading.Tasks;
using Player = SmashLeague.Data.Player;

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
