using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using SmashLeague.Data;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace SmashLeague.Data.Migrations
{
    [DbContext(typeof(SmashLeagueDbContext))]
    partial class ImageHeaderToTeam
    {
        public override string Id
        {
            get { return "20150908164733_ImageHeaderToTeam"; }
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
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

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId");

                    b.Key("LoginProvider", "ProviderKey");

                    b.Annotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Key("UserId", "RoleId");

                    b.Annotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("SmashLeague.Data.ApplicationUser", b =>
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

                    b.Property<int?>("HeaderImageImageId");

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

                    b.Property<int?>("ProfileImageImageId");

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

            modelBuilder.Entity("SmashLeague.Data.DefaultImages", b =>
                {
                    b.Property<string>("Name");

                    b.Property<int?>("ImageImageId")
                        .Required();

                    b.Key("Name");
                });

            modelBuilder.Entity("SmashLeague.Data.DefaultSeason", b =>
                {
                    b.Property<string>("Name");

                    b.Property<int?>("SeasonSeasonId")
                        .Required();

                    b.Key("Name");
                });

            modelBuilder.Entity("SmashLeague.Data.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Source")
                        .Required();

                    b.Key("ImageId");
                });

            modelBuilder.Entity("SmashLeague.Data.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("SeasonSeasonId");

                    b.Property<int?>("SeriesSeriesId");

                    b.Property<int?>("WinnerTeamId");

                    b.Key("MatchId");
                });

            modelBuilder.Entity("SmashLeague.Data.Matchup", b =>
                {
                    b.Property<int>("MatchId");

                    b.Property<int>("TeamId");

                    b.Key("MatchId", "TeamId");
                });

            modelBuilder.Entity("SmashLeague.Data.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Message");

                    b.Property<bool>("Read");

                    b.Property<string>("Title");

                    b.Property<string>("UserId");

                    b.Key("NotificationId");
                });

            modelBuilder.Entity("SmashLeague.Data.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("LookingForTeam");

                    b.Property<int?>("PreferredRole");

                    b.Property<int?>("RankRankId");

                    b.Property<string>("Tag");

                    b.Property<string>("UserId");

                    b.Key("PlayerId");
                });

            modelBuilder.Entity("SmashLeague.Data.Rank", b =>
                {
                    b.Property<int>("RankId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Position");

                    b.Property<int?>("RankBracketSeasonId")
                        .Required();

                    b.Property<byte?>("RankBracketType")
                        .Required();

                    b.Property<int?>("RatingRatingId")
                        .Required();

                    b.Key("RankId");
                });

            modelBuilder.Entity("SmashLeague.Data.RankBracket", b =>
                {
                    b.Property<byte>("Type")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SeasonId");

                    b.Property<int>("MaximumMMR");

                    b.Property<int>("MinimumMMR");

                    b.Property<string>("Name")
                        .Required();

                    b.Key("Type", "SeasonId");
                });

            modelBuilder.Entity("SmashLeague.Data.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MatchMakingRating");

                    b.Key("RatingId");
                });

            modelBuilder.Entity("SmashLeague.Data.Season", b =>
                {
                    b.Property<int>("SeasonId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .Required();

                    b.Key("SeasonId");
                });

            modelBuilder.Entity("SmashLeague.Data.Series", b =>
                {
                    b.Property<int>("SeriesId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MatchCount");

                    b.Property<int?>("SeasonSeasonId");

                    b.Property<int?>("WinnerPlayerId");

                    b.Property<int?>("WinnerTeamId");

                    b.Key("SeriesId");
                });

            modelBuilder.Entity("SmashLeague.Data.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("HeaderImageImageId");

                    b.Property<string>("Name")
                        .Required()
                        .Annotation("MaxLength", 50);

                    b.Property<string>("NormalizedName")
                        .Required()
                        .Annotation("MaxLength", 50);

                    b.Property<int?>("RankRankId");

                    b.Property<int?>("TeamImageImageId");

                    b.Key("TeamId");

                    b.AlternateKey("Name");


                    b.AlternateKey("NormalizedName");

                    b.Index("Name");

                    b.Annotation("Relational:TableName", "Teams");
                });

            modelBuilder.Entity("SmashLeague.Data.TeamInvite", b =>
                {
                    b.Property<int>("TeamId");

                    b.Property<int>("PlayerId");

                    b.Key("TeamId", "PlayerId");
                });

            modelBuilder.Entity("SmashLeague.Data.TeamOwner", b =>
                {
                    b.Property<int>("TeamId");

                    b.Property<int>("PlayerId");

                    b.Key("TeamId", "PlayerId");
                });

            modelBuilder.Entity("SmashLeague.Data.TeamPlayer", b =>
                {
                    b.Property<int>("PlayerId");

                    b.Property<int>("TeamId");

                    b.Key("PlayerId", "TeamId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Reference("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .InverseCollection()
                        .ForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Reference("SmashLeague.Data.ApplicationUser")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Reference("SmashLeague.Data.ApplicationUser")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Reference("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .InverseCollection()
                        .ForeignKey("RoleId");

                    b.Reference("SmashLeague.Data.ApplicationUser")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("SmashLeague.Data.ApplicationUser", b =>
                {
                    b.Reference("SmashLeague.Data.Image")
                        .InverseCollection()
                        .ForeignKey("HeaderImageImageId");

                    b.Reference("SmashLeague.Data.Image")
                        .InverseCollection()
                        .ForeignKey("ProfileImageImageId");
                });

            modelBuilder.Entity("SmashLeague.Data.DefaultImages", b =>
                {
                    b.Reference("SmashLeague.Data.Image")
                        .InverseCollection()
                        .ForeignKey("ImageImageId");
                });

            modelBuilder.Entity("SmashLeague.Data.DefaultSeason", b =>
                {
                    b.Reference("SmashLeague.Data.Season")
                        .InverseCollection()
                        .ForeignKey("SeasonSeasonId");
                });

            modelBuilder.Entity("SmashLeague.Data.Match", b =>
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

            modelBuilder.Entity("SmashLeague.Data.Matchup", b =>
                {
                    b.Reference("SmashLeague.Data.Match")
                        .InverseCollection()
                        .ForeignKey("MatchId");

                    b.Reference("SmashLeague.Data.Team")
                        .InverseCollection()
                        .ForeignKey("TeamId");
                });

            modelBuilder.Entity("SmashLeague.Data.Notification", b =>
                {
                    b.Reference("SmashLeague.Data.ApplicationUser")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("SmashLeague.Data.Player", b =>
                {
                    b.Reference("SmashLeague.Data.Rank")
                        .InverseCollection()
                        .ForeignKey("RankRankId");

                    b.Reference("SmashLeague.Data.ApplicationUser")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("SmashLeague.Data.Rank", b =>
                {
                    b.Reference("SmashLeague.Data.Rating")
                        .InverseCollection()
                        .ForeignKey("RatingRatingId");

                    b.Reference("SmashLeague.Data.RankBracket")
                        .InverseCollection()
                        .ForeignKey("RankBracketType", "RankBracketSeasonId");
                });

            modelBuilder.Entity("SmashLeague.Data.RankBracket", b =>
                {
                    b.Reference("SmashLeague.Data.Season")
                        .InverseCollection()
                        .ForeignKey("SeasonId");
                });

            modelBuilder.Entity("SmashLeague.Data.Series", b =>
                {
                    b.Reference("SmashLeague.Data.Season")
                        .InverseCollection()
                        .ForeignKey("SeasonSeasonId");

                    b.Reference("SmashLeague.Data.TeamPlayer")
                        .InverseCollection()
                        .ForeignKey("WinnerPlayerId", "WinnerTeamId");
                });

            modelBuilder.Entity("SmashLeague.Data.Team", b =>
                {
                    b.Reference("SmashLeague.Data.Image")
                        .InverseCollection()
                        .ForeignKey("HeaderImageImageId");

                    b.Reference("SmashLeague.Data.Rank")
                        .InverseCollection()
                        .ForeignKey("RankRankId");

                    b.Reference("SmashLeague.Data.Image")
                        .InverseCollection()
                        .ForeignKey("TeamImageImageId");
                });

            modelBuilder.Entity("SmashLeague.Data.TeamInvite", b =>
                {
                    b.Reference("SmashLeague.Data.Player")
                        .InverseCollection()
                        .ForeignKey("PlayerId");

                    b.Reference("SmashLeague.Data.Team")
                        .InverseCollection()
                        .ForeignKey("TeamId");
                });

            modelBuilder.Entity("SmashLeague.Data.TeamOwner", b =>
                {
                    b.Reference("SmashLeague.Data.Player")
                        .InverseCollection()
                        .ForeignKey("PlayerId");

                    b.Reference("SmashLeague.Data.Team")
                        .InverseReference()
                        .ForeignKey("SmashLeague.Data.TeamOwner", "TeamId");
                });

            modelBuilder.Entity("SmashLeague.Data.TeamPlayer", b =>
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
