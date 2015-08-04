using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace SmashLeague.Data
{
    public class SmashLeagueDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<RankBracket> RankBrackets { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }

        public SmashLeagueDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Team>()
                .ToTable("Teams")
                .Index(team => team.Name);
        }
    }
}
