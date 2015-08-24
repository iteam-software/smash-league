using SmashLeague.Data;
using Microsoft.Data.Entity;
using System.Threading.Tasks;
using System.Linq;

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

        public async Task<Player> GetPlayerByUserNameAsync(string username)
        {
            var player = await _db.Players
                .Include(x => x.User).ThenInclude(y => y.HeaderImage)
                .Include(x => x.User).ThenInclude(y => y.ProfileImage)
                .FirstOrDefaultAsync(x => x.User.UserName == username);

            return player;
        }

        public async Task<Player[]> GetPlayersAsync(int? max = default(int?))
        {
            if (max == null)
            {
                return await _db.Players
                    .Include(x => x.User).ThenInclude(y => y.HeaderImage)
                    .Include(x => x.User).ThenInclude(y => y.ProfileImage)
                    .ToArrayAsync();
            }
            else
            {
                return await _db.Players
                    .Include(x => x.User).ThenInclude(y => y.HeaderImage)
                    .Include(x => x.User).ThenInclude(y => y.ProfileImage)
                    .Take(max.Value)
                    .ToArrayAsync();
            }
        }
    }
}
