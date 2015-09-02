using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using SmashLeague.Data;

namespace SmashLeagueDataMigrations
{
    [ContextType(typeof(SmashLeagueDbContext))]
    partial class SmashLeagueDbContextModelSnapshot : ModelSnapshot
    {
        public override void BuildModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13815")
                .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

            builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .ConcurrencyToken();

                    b.Property<string>("Name")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .Annotation("MaxLength", 256);

                    b.Key("Id");

                    b.Index("NormalizedName")
                        .Annotation("Relational:Name", "RoleNameIndex");

                    b.Annotation("Relational:TableName", "AspNetRoles");
                });

            builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "AspNetRoleClaims");
                });

            builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "AspNetUserClaims");
                });

            builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId");

                    b.Key("LoginProvider", "ProviderKey");

                    b.Annotation("Relational:TableName", "AspNetUserLogins");
                });

            builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Key("UserId", "RoleId");

                    b.Annotation("Relational:TableName", "AspNetUserRoles");
                });

            builder.Entity("SmashLeague.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Battletag")
                        .Required();

                    b.Property<DateTime?>("Birthday");

                    b.Property<string>("ConcurrencyStamp")
                        .ConcurrencyToken();

                    b.Property<string>("Email")
                        .Annotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("EmailNotifications");

                    b.Property<int?>("HeaderImageProfileImageId");

                    b.Property<string>("Location");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int?>("ProfileImageProfileImageId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .Annotation("MaxLength", 256);

                    b.Key("Id");

                    b.Index("NormalizedEmail")
                        .Annotation("Relational:Name", "EmailIndex");

                    b.Index("NormalizedUserName")
                        .Annotation("Relational:Name", "UserNameIndex");

                    b.Annotation("Relational:TableName", "AspNetUsers");
                });

            builder.Entity("SmashLeague.Data.DefaultImages", b =>
                {
                    b.Property<string>("Name");

                    b.Property<int?>("ImageProfileImageId");

                    b.Key("Name");
                });

            builder.Entity("SmashLeague.Data.Image", b =>
                {
                    b.Property<int>("ProfileImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Source")
                        .Required();

                    b.Key("ProfileImageId");
                });

            builder.Entity("SmashLeague.Data.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("SeasonSeasonId");

                    b.Property<int?>("SeriesSeriesId");

                    b.Property<int?>("WinnerTeamId");

                    b.Key("MatchId");
                });

            builder.Entity("SmashLeague.Data.Matchup", b =>
                {
                    b.Property<int>("MatchId");

                    b.Property<int>("TeamId");

                    b.Key("MatchId", "TeamId");
                });

            builder.Entity("SmashLeague.Data.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Message");

                    b.Property<bool>("Read");

                    b.Property<string>("Title");

                    b.Property<string>("UserId");

                    b.Key("NotificationId");
                });

            builder.Entity("SmashLeague.Data.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("LookingForTeam");

                    b.Property<int?>("PreferredRole");

                    b.Property<string>("Tag");

                    b.Property<string>("UserId");

                    b.Key("PlayerId");
                });

            builder.Entity("SmashLeague.Data.Rank", b =>
                {
                    b.Property<int>("RankId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PlayerPlayerId");

                    b.Property<int>("Position");

                    b.Property<int?>("RankBracketRankingBracketId");

                    b.Property<int?>("RatingRatingId");

                    b.Property<int?>("TeamPlayerId");

                    b.Property<int?>("TeamTeamId");

                    b.Property<int?>("TeamTeamId1");

                    b.Key("RankId");
                });

            builder.Entity("SmashLeague.Data.RankBracket", b =>
                {
                    b.Property<int>("RankingBracketId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MaximumMMR");

                    b.Property<int>("MinimumMMR");

                    b.Property<string>("Name")
                        .Required();

                    b.Property<int?>("SeasonSeasonId");

                    b.Key("RankingBracketId");
                });

            builder.Entity("SmashLeague.Data.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MatchMakingRating");

                    b.Key("RatingId");
                });

            builder.Entity("SmashLeague.Data.Season", b =>
                {
                    b.Property<int>("SeasonId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .Required();

                    b.Key("SeasonId");
                });

            builder.Entity("SmashLeague.Data.Series", b =>
                {
                    b.Property<int>("SeriesId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MatchCount");

                    b.Property<int?>("SeasonSeasonId");

                    b.Property<int?>("WinnerPlayerId");

                    b.Property<int?>("WinnerTeamId");

                    b.Key("SeriesId");
                });

            builder.Entity("SmashLeague.Data.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .Required()
                        .Annotation("MaxLength", 50);

                    b.Property<string>("NormalizedName")
                        .Required()
                        .Annotation("MaxLength", 50);

                    b.Key("TeamId");

                    b.Index("Name");

                    b.Annotation("Relational:TableName", "Teams");
                });

            builder.Entity("SmashLeague.Data.TeamInvite", b =>
                {
                    b.Property<int>("TeamId");

                    b.Property<int>("PlayerId");

                    b.Key("TeamId", "PlayerId");
                });

            builder.Entity("SmashLeague.Data.TeamOwner", b =>
                {
                    b.Property<int>("TeamId");

                    b.Property<int>("PlayerId");

                    b.Key("TeamId", "PlayerId");
                });

            builder.Entity("SmashLeague.Data.TeamPlayer", b =>
                {
                    b.Property<int>("PlayerId");

                    b.Property<int>("TeamId");

                    b.Key("PlayerId", "TeamId");
                });

            builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Reference("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .InverseCollection()
                        .ForeignKey("RoleId");
                });

            builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Reference("SmashLeague.Data.ApplicationUser")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Reference("SmashLeague.Data.ApplicationUser")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            builder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Reference("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .InverseCollection()
                        .ForeignKey("RoleId");

                    b.Reference("SmashLeague.Data.ApplicationUser")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            builder.Entity("SmashLeague.Data.ApplicationUser", b =>
                {
                    b.Reference("SmashLeague.Data.Image")
                        .InverseCollection()
                        .ForeignKey("HeaderImageProfileImageId");

                    b.Reference("SmashLeague.Data.Image")
                        .InverseCollection()
                        .ForeignKey("ProfileImageProfileImageId");
                });

            builder.Entity("SmashLeague.Data.DefaultImages", b =>
                {
                    b.Reference("SmashLeague.Data.Image")
                        .InverseCollection()
                        .ForeignKey("ImageProfileImageId");
                });

            builder.Entity("SmashLeague.Data.Match", b =>
                {
                    b.Reference("SmashLeague.Data.Season")
                        .InverseCollection()
                        .ForeignKey("SeasonSeasonId");

                    b.Reference("SmashLeague.Data.Series")
                        .InverseCollection()
                        .ForeignKey("SeriesSeriesId");

                    b.Reference("SmashLeague.Data.Team")
                        .InverseCollection()
                        .ForeignKey("WinnerTeamId");
                });

            builder.Entity("SmashLeague.Data.Matchup", b =>
                {
                    b.Reference("SmashLeague.Data.Match")
                        .InverseCollection()
                        .ForeignKey("MatchId");

                    b.Reference("SmashLeague.Data.Team")
                        .InverseCollection()
                        .ForeignKey("TeamId");
                });

            builder.Entity("SmashLeague.Data.Notification", b =>
                {
                    b.Reference("SmashLeague.Data.ApplicationUser")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            builder.Entity("SmashLeague.Data.Player", b =>
                {
                    b.Reference("SmashLeague.Data.ApplicationUser")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            builder.Entity("SmashLeague.Data.Rank", b =>
                {
                    b.Reference("SmashLeague.Data.Player")
                        .InverseCollection()
                        .ForeignKey("PlayerPlayerId");

                    b.Reference("SmashLeague.Data.RankBracket")
                        .InverseCollection()
                        .ForeignKey("RankBracketRankingBracketId");

                    b.Reference("SmashLeague.Data.Rating")
                        .InverseCollection()
                        .ForeignKey("RatingRatingId");

                    b.Reference("SmashLeague.Data.Team")
                        .InverseCollection()
                        .ForeignKey("TeamTeamId");

                    b.Reference("SmashLeague.Data.TeamPlayer")
                        .InverseCollection()
                        .ForeignKey("TeamPlayerId", "TeamTeamId1");
                });

            builder.Entity("SmashLeague.Data.RankBracket", b =>
                {
                    b.Reference("SmashLeague.Data.Season")
                        .InverseCollection()
                        .ForeignKey("SeasonSeasonId");
                });

            builder.Entity("SmashLeague.Data.Series", b =>
                {
                    b.Reference("SmashLeague.Data.Season")
                        .InverseCollection()
                        .ForeignKey("SeasonSeasonId");

                    b.Reference("SmashLeague.Data.TeamPlayer")
                        .InverseCollection()
                        .ForeignKey("WinnerPlayerId", "WinnerTeamId");
                });

            builder.Entity("SmashLeague.Data.TeamInvite", b =>
                {
                    b.Reference("SmashLeague.Data.Player")
                        .InverseCollection()
                        .ForeignKey("PlayerId");

                    b.Reference("SmashLeague.Data.Team")
                        .InverseCollection()
                        .ForeignKey("TeamId");
                });

            builder.Entity("SmashLeague.Data.TeamOwner", b =>
                {
                    b.Reference("SmashLeague.Data.Player")
                        .InverseCollection()
                        .ForeignKey("PlayerId");

                    b.Reference("SmashLeague.Data.Team")
                        .InverseReference()
                        .ForeignKey("SmashLeague.Data.TeamOwner", "TeamId");
                });

            builder.Entity("SmashLeague.Data.TeamPlayer", b =>
                {
                    b.Reference("SmashLeague.Data.Player")
                        .InverseCollection()
                        .ForeignKey("PlayerId");

                    b.Reference("SmashLeague.Data.Team")
                        .InverseCollection()
                        .ForeignKey("TeamId");
                });
        }
    }
}
