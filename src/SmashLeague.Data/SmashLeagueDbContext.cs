using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace SmashLeague.Data
{
    public class SmashLeagueDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<RankedTeam> RankedTeams { get; set; }
        //public DbSet<Season> Seasons { get; set; }
        //public DbSet<RankingBracket> RankingBrackets { get; set; }
        //public DbSet<Series> Series { get; set; }
        //public DbSet<Match> Matches { get; set; }
        //public DbSet<TeamUser> TeamUsers { get; set; }
        //public DbSet<RankedUser> RankedUsers { get; set; }

        public SmashLeagueDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Team>().ToTable("Teams")
                .Index(team => team.Name);

            builder.Entity<Rank>()
                .ToTable("Ranks");

            builder.Entity<RankedTeam>()
                .ToTable("RankedTeams")
                .BaseType<Team>();

            //builder.Entity<Season>().ToTable("Seasons");
            //builder.Entity<RankingBracket>().ToTable("RankingBrackets");
            //builder.Entity<Series>().ToTable("Series");
            //builder.Entity<Match>().ToTable("Matches");
            //builder.Entity<TeamUser>().ToTable("TeamUsers");
            //builder.Entity<RankedUser>().ToTable("RankedUsers");
        }
    }
}
