using Microsoft.Data.Entity;
using SmashLeague.Data;
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

        public async Task CreateNewTeamRankingAsync(Team team, Season season = null)
        {
            // Find the worst rank in the basic bracket
            var neverLucky = await _db.RankBrackets
                .Include(x => x.Season)
                .FirstOrDefaultAsync(x => x.Type == RankBrackets.NeverLucky && x.Season == season);

            var worstRank = await _db.Ranks
                .Include(x => x.RankBracket)
                .OrderByDescending(x => x.Position)
                .FirstOrDefaultAsync(x => x.RankBracket == neverLucky);

            var rank = new Rank
            {
                RankBracket = neverLucky,
                Position = worstRank != null ? worstRank.Position + 1 : 1,
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
