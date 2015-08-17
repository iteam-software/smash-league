using SmashLeague.Data;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public interface IPlayerManager
    {
        Task<Player> CreatePlayerForUserAsync(ApplicationUser user);
    }
}
