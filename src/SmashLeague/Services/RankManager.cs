using Microsoft.Data.Entity;
using SmashLeague.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public class RankManager : IRankManager
    {
        private readonly SmashLeagueDbContext _db;

        public RankManager(
            SmashLeagueDbContext db)
        {
            _db = db;
        }

        public async Task CreateNewTeamRankingAsync(Team team, Season season)
        {
            if (team == null)
            {
                throw new ArgumentNullException(nameof(team));
            }
            if (season == null)
            {
                throw new ArgumentNullException(nameof(season));
            }

            // Find the worst rank in the basic bracket
            var neverLucky = await _db.RankBrackets
                    .Include(x => x.Season)
                    .SingleOrDefaultAsync(x => x.Type == RankBrackets.NeverLucky && x.Season.SeasonId == season.SeasonId);

            if (neverLucky == null)
            {
                throw new InvalidProgramException("Unable to load baseline bracket for new team");
            }

            var rank = new Rank
            {
                RankBracket = neverLucky,
                Rating = new Rating { MatchMakingRating = 1500 }
            };

            team.Rank = rank;

            _db.Add(rank);
            _db.Add(rank.Rating);
            _db.Update(team);

            await _db.SaveChangesAsync();
        }
    }
}
