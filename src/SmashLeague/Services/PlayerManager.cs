using SmashLeague.Data;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public class PlayerManager : IPlayerManager
    {
        SmashLeagueDbContext _db;

        public PlayerManager(SmashLeagueDbContext db)
        {
            _db = db;
        }

        public async Task<Player> CreatePlayerForUserAsync(ApplicationUser user)
        {
            var player = new Player { User = user };

            _db.Players.Add(player);
            await _db.SaveChangesAsync();

            return player;
        }
    }
}
