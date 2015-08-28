using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace SmashLeague.Data
{
    public class SmashLeagueDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<TeamPlayer> Teams { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<RankBracket> RankBrackets { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Matchup> Matchups { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }
        public DbSet<TeamPotentialPlayer> TeamPotentialPlayers { get; set; }
        public DbSet<TeamOwner> TeamOwners { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<DefaultImages> DefaultImages { get; set; }

        public SmashLeagueDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Team>()
                .ToTable("Teams")
                .Index(team => team.Name);

            builder.Entity<Matchup>()
                .Key(x => new { x.MatchId, x.TeamId });
            builder.Entity<Matchup>()
                .Reference(x => x.Match)
                .InverseCollection(x => x.Matchups);
            builder.Entity<Matchup>()
                .Reference(x => x.Team)
                .InverseCollection(x => x.Matchups);

            builder.Entity<Match>()
                .Reference(x => x.Winner)
                .InverseCollection(x => x.Wins);

            builder.Entity<TeamPlayer>()
                .Key(x => new { x.PlayerId, x.TeamId });
            builder.Entity<TeamPlayer>()
                .Reference(x => x.Team)
                .InverseCollection(x => x.Members);
            builder.Entity<TeamPlayer>()
                .Reference(x => x.Player)
                .InverseCollection(x => x.Teams);

            builder.Entity<TeamPotentialPlayer>()
                .Key(x => new { x.PlayerId, x.TeamId });
            builder.Entity<TeamPotentialPlayer>()
                .Reference(x => x.Team)
                .InverseCollection(x => x.PotentialMember);
            builder.Entity<TeamPotentialPlayer>()
                .Reference(x => x.Player)
                .InverseCollection(x => x.PotentialTeams);

            builder.Entity<TeamOwner>()
                .Key(x => new { x.TeamId, x.PlayerId });
            builder.Entity<TeamOwner>()
                .Reference(x => x.Team)
                .InverseReference(x => x.Owner);
            builder.Entity<TeamOwner>()
                .Reference(x => x.Player)
                .InverseCollection(x => x.OwnedTeams);
        }
    }
}
