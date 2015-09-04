using Microsoft.Data.Entity;
using SmashLeague.Data;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public class SeasonManager : ISeasonManager
    {
        private readonly SmashLeagueDbContext _db;

        public SeasonManager(
            SmashLeagueDbContext db)
        {
            _db = db;
        }

        public async Task<Season> GetCurrentSeasonAsync()
        {
            var defaultSeason = await _db.DefaultSeasons
                .Include(x => x.Season).ThenInclude(x => x.Brackets)
                .SingleOrDefaultAsync(x => x.Name == Defaults.DefaultSeason);

            return defaultSeason != null ? defaultSeason.Season : null;
        }
    }
}
