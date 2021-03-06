﻿using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace SmashLeague.Data
{
    public class SmashLeagueDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<DefaultSeason> DefaultSeasons { get; set; }
        public DbSet<RankBracket> RankBrackets { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Matchup> Matchups { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }
        public DbSet<TeamInvite> TeamPotentialPlayers { get; set; }
        public DbSet<TeamOwner> TeamOwners { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<DefaultImages> DefaultImages { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public SmashLeagueDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Notification>()
                .Reference(x => x.User)
                .InverseCollection(x => x.Notifications);

            builder.Entity<Team>()
                .ToTable("Teams")
                .Index(team => team.Name);
            builder.Entity<Team>()
                .AlternateKey(x => x.Name);
            builder.Entity<Team>()
                .AlternateKey(x => x.NormalizedName);

            builder.Entity<RankBracket>()
                .Reference(x => x.Season)
                .InverseCollection(x => x.Brackets);
            builder.Entity<RankBracket>()
                .Key(x => new { x.Type, x.SeasonId });
            builder.Entity<RankBracket>()
                .Property(x => x.Type)
                .ValueGeneratedOnAdd();
            var rankBracketMetadata = builder.Entity<RankBracket>().Property(x => x.Type).Metadata;
            rankBracketMetadata.SentinelValue = -1;
            rankBracketMetadata.IsReadOnlyAfterSave = true;
            rankBracketMetadata.IsReadOnlyBeforeSave = false;

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

            builder.Entity<TeamOwner>()
                .Key(x => new { x.TeamId, x.PlayerId });
            builder.Entity<TeamOwner>()
                .Reference(x => x.Team)
                .InverseReference(x => x.Owner);
            builder.Entity<TeamOwner>()
                .Reference(x => x.Player)
                .InverseCollection(x => x.OwnedTeams);

            builder.Entity<TeamInvite>()
                .Key(x => new { x.TeamId, x.PlayerId });
            builder.Entity<TeamInvite>()
                .Reference(x => x.Team)
                .InverseCollection(x => x.Invitees);
            builder.Entity<TeamInvite>()
                .Reference(x => x.Player)
                .InverseCollection(x => x.Invites);
        }
    }
}
