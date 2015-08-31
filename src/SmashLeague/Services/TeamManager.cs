using SmashLeague.Data;
using System;
using Microsoft.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public class TeamManager : ITeamManager
    {
        private readonly SmashLeagueDbContext _db;

        public TeamManager(SmashLeagueDbContext db)
        {
            _db = db;
        }

        public async Task<Data.Player[]> Suggest(DataTransferObjects.Player[] players)
        {
            var suggestions = await _db.Players
                .Include(x => x.User).ThenInclude(x => x.ProfileImage)
                .Include(x => x.User).ThenInclude(x => x.HeaderImage)
                .Where(x => x.LookingForTeam)
                .ToArrayAsync();

            return suggestions;
        }
    }
}
