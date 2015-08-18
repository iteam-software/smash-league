using SmashLeague.Data;
using SmashLeague.DataTransferObjects;
using System.Linq;
using Microsoft.Data.Entity;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public class MatchManager : IMatchManager
    {
        private readonly SmashLeagueDbContext _db;

        public MatchManager(SmashLeagueDbContext db)
        {
            _db = db;
        }

        public async Task<MatchDto[]> GetMatchDtoAsync(bool? completed = null)
        {
            Match[] data;
            if (completed != null)
            {
                var _completed = (bool)completed;
                data = await _db.Matches
                    .Include(x => x.Winner)
                    .Include(x => x.Series)
                    .Include(x => x.Matchups).ThenInclude(m => m.Team)
                    .Where(x => _completed ? x.Winner != null : x.Winner == null)
                    .ToArrayAsync();
            }
            else
            {
                data = await _db.Matches
                    .Include(x => x.Winner)
                    .Include(x => x.Series)
                    .Include(x => x.Matchups).ThenInclude(m => m.Team)
                    .ToArrayAsync();
            }

            return data.Select(x => new MatchDto
            {
                MatchId = x.MatchId,
                Teams = x.Matchups.Select(m => new TeamLazyDto { Name = m.Team.Name, TeamId = m.TeamId }).ToArray(),
                WinnerId = x.Winner != null ? x.Winner.TeamId : 0,
                SeriesId = x.Series != null ? x.Series.SeriesId : 0
            }).ToArray();
        }
    }
}
