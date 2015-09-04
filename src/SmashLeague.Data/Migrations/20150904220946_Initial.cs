using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace SmashLeague.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(isNullable: false),
                    ConcurrencyStamp = table.Column<string>(isNullable: true),
                    Name = table.Column<string>(isNullable: true),
                    NormalizedName = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    Source = table.Column<string>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                });
            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    MatchMakingRating = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingId);
                });
            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    SeasonId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    Name = table.Column<string>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.SeasonId);
                });
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(isNullable: true),
                    ClaimValue = table.Column<string>(isNullable: true),
                    RoleId = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(isNullable: false),
                    AccessFailedCount = table.Column<int>(isNullable: false),
                    Battletag = table.Column<string>(isNullable: false),
                    Birthday = table.Column<DateTime>(isNullable: true),
                    ConcurrencyStamp = table.Column<string>(isNullable: true),
                    Email = table.Column<string>(isNullable: true),
                    EmailConfirmed = table.Column<bool>(isNullable: false),
                    EmailNotifications = table.Column<bool>(isNullable: false),
                    HeaderImageImageId = table.Column<int>(isNullable: true),
                    Location = table.Column<string>(isNullable: true),
                    LockoutEnabled = table.Column<bool>(isNullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(isNullable: true),
                    Name = table.Column<string>(isNullable: true),
                    NormalizedEmail = table.Column<string>(isNullable: true),
                    NormalizedUserName = table.Column<string>(isNullable: true),
                    PasswordHash = table.Column<string>(isNullable: true),
                    PhoneNumber = table.Column<string>(isNullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(isNullable: false),
                    ProfileImageImageId = table.Column<int>(isNullable: true),
                    SecurityStamp = table.Column<string>(isNullable: true),
                    TwoFactorEnabled = table.Column<bool>(isNullable: false),
                    UserName = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_Image_HeaderImageImageId",
                        column: x => x.HeaderImageImageId,
                        principalTable: "Image",
                        principalColumn: "ImageId");
                    table.ForeignKey(
                        name: "FK_ApplicationUser_Image_ProfileImageImageId",
                        column: x => x.ProfileImageImageId,
                        principalTable: "Image",
                        principalColumn: "ImageId");
                });
            migrationBuilder.CreateTable(
                name: "DefaultImages",
                columns: table => new
                {
                    Name = table.Column<string>(isNullable: false),
                    ImageImageId = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultImages", x => x.Name);
                    table.ForeignKey(
                        name: "FK_DefaultImages_Image_ImageImageId",
                        column: x => x.ImageImageId,
                        principalTable: "Image",
                        principalColumn: "ImageId");
                });
            migrationBuilder.CreateTable(
                name: "DefaultSeason",
                columns: table => new
                {
                    Name = table.Column<string>(isNullable: false),
                    SeasonSeasonId = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultSeason", x => x.Name);
                    table.ForeignKey(
                        name: "FK_DefaultSeason_Season_SeasonSeasonId",
                        column: x => x.SeasonSeasonId,
                        principalTable: "Season",
                        principalColumn: "SeasonId");
                });
            migrationBuilder.CreateTable(
                name: "RankBracket",
                columns: table => new
                {
                    Type = table.Column<byte>(isNullable: false),
                    SeasonId = table.Column<int>(isNullable: false),
                    MaximumMMR = table.Column<int>(isNullable: false),
                    MinimumMMR = table.Column<int>(isNullable: false),
                    Name = table.Column<string>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankBracket", x => new { x.Type, x.SeasonId });
                    table.ForeignKey(
                        name: "FK_RankBracket_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "SeasonId");
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(isNullable: true),
                    ClaimValue = table.Column<string>(isNullable: true),
                    UserId = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(isNullable: false),
                    ProviderKey = table.Column<string>(isNullable: false),
                    ProviderDisplayName = table.Column<string>(isNullable: true),
                    UserId = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin<string>", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(isNullable: false),
                    RoleId = table.Column<string>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    Message = table.Column<string>(isNullable: true),
                    Read = table.Column<bool>(isNullable: false),
                    Title = table.Column<string>(isNullable: true),
                    UserId = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "Rank",
                columns: table => new
                {
                    RankId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    Position = table.Column<int>(isNullable: false),
                    RankBracketSeasonId = table.Column<int>(isNullable: false),
                    RankBracketType = table.Column<byte>(isNullable: false),
                    RatingRatingId = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rank", x => x.RankId);
                    table.ForeignKey(
                        name: "FK_Rank_Rating_RatingRatingId",
                        column: x => x.RatingRatingId,
                        principalTable: "Rating",
                        principalColumn: "RatingId");
                    table.ForeignKey(
                        name: "FK_Rank_RankBracket_RankBracketType_RankBracketSeasonId",
                        columns: x => new { x.RankBracketType, x.RankBracketSeasonId },
                        principalTable: "RankBracket",
                        principalColumns: new[] { "Type", "SeasonId" });
                });
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    LookingForTeam = table.Column<bool>(isNullable: false),
                    PreferredRole = table.Column<int>(isNullable: true),
                    RankRankId = table.Column<int>(isNullable: true),
                    Tag = table.Column<string>(isNullable: true),
                    UserId = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Player_Rank_RankRankId",
                        column: x => x.RankRankId,
                        principalTable: "Rank",
                        principalColumn: "RankId");
                    table.ForeignKey(
                        name: "FK_Player_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    Name = table.Column<string>(isNullable: false),
                    NormalizedName = table.Column<string>(isNullable: false),
                    RankRankId = table.Column<int>(isNullable: true),
                    TeamImageImageId = table.Column<int>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamId);
                    table.UniqueConstraint("AK_Team_Name", x => x.Name);
                    table.UniqueConstraint("AK_Team_NormalizedName", x => x.NormalizedName);
                    table.ForeignKey(
                        name: "FK_Team_Rank_RankRankId",
                        column: x => x.RankRankId,
                        principalTable: "Rank",
                        principalColumn: "RankId");
                    table.ForeignKey(
                        name: "FK_Team_Image_TeamImageImageId",
                        column: x => x.TeamImageImageId,
                        principalTable: "Image",
                        principalColumn: "ImageId");
                });
            migrationBuilder.CreateTable(
                name: "TeamInvite",
                columns: table => new
                {
                    TeamId = table.Column<int>(isNullable: false),
                    PlayerId = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamInvite", x => new { x.TeamId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_TeamInvite_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId");
                    table.ForeignKey(
                        name: "FK_TeamInvite_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                });
            migrationBuilder.CreateTable(
                name: "TeamOwner",
                columns: table => new
                {
                    TeamId = table.Column<int>(isNullable: false),
                    PlayerId = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamOwner", x => new { x.TeamId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_TeamOwner_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId");
                    table.ForeignKey(
                        name: "FK_TeamOwner_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                });
            migrationBuilder.CreateTable(
                name: "TeamPlayer",
                columns: table => new
                {
                    PlayerId = table.Column<int>(isNullable: false),
                    TeamId = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayer", x => new { x.PlayerId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId");
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                });
            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    SeriesId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    MatchCount = table.Column<int>(isNullable: false),
                    SeasonSeasonId = table.Column<int>(isNullable: true),
                    WinnerPlayerId = table.Column<int>(isNullable: true),
                    WinnerTeamId = table.Column<int>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.SeriesId);
                    table.ForeignKey(
                        name: "FK_Series_Season_SeasonSeasonId",
                        column: x => x.SeasonSeasonId,
                        principalTable: "Season",
                        principalColumn: "SeasonId");
                    table.ForeignKey(
                        name: "FK_Series_TeamPlayer_WinnerPlayerId_WinnerTeamId",
                        columns: x => new { x.WinnerPlayerId, x.WinnerTeamId },
                        principalTable: "TeamPlayer",
                        principalColumns: new[] { "PlayerId", "TeamId" });
                });
            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    MatchId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    SeasonSeasonId = table.Column<int>(isNullable: true),
                    SeriesSeriesId = table.Column<int>(isNullable: true),
                    WinnerTeamId = table.Column<int>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Match_Season_SeasonSeasonId",
                        column: x => x.SeasonSeasonId,
                        principalTable: "Season",
                        principalColumn: "SeasonId");
                    table.ForeignKey(
                        name: "FK_Match_Series_SeriesSeriesId",
                        column: x => x.SeriesSeriesId,
                        principalTable: "Series",
                        principalColumn: "SeriesId");
                    table.ForeignKey(
                        name: "FK_Match_Team_WinnerTeamId",
                        column: x => x.WinnerTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                });
            migrationBuilder.CreateTable(
                name: "Matchup",
                columns: table => new
                {
                    MatchId = table.Column<int>(isNullable: false),
                    TeamId = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matchup", x => new { x.MatchId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_Matchup_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "MatchId");
                    table.ForeignKey(
                        name: "FK_Matchup_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                });
            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");
            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName");
            migrationBuilder.CreateIndex(
                name: "IX_Team_Name",
                table: "Teams",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("AspNetRoleClaims");
            migrationBuilder.DropTable("AspNetUserClaims");
            migrationBuilder.DropTable("AspNetUserLogins");
            migrationBuilder.DropTable("AspNetUserRoles");
            migrationBuilder.DropTable("DefaultImages");
            migrationBuilder.DropTable("DefaultSeason");
            migrationBuilder.DropTable("Matchup");
            migrationBuilder.DropTable("Notification");
            migrationBuilder.DropTable("TeamInvite");
            migrationBuilder.DropTable("TeamOwner");
            migrationBuilder.DropTable("AspNetRoles");
            migrationBuilder.DropTable("Match");
            migrationBuilder.DropTable("Series");
            migrationBuilder.DropTable("TeamPlayer");
            migrationBuilder.DropTable("Player");
            migrationBuilder.DropTable("Teams");
            migrationBuilder.DropTable("AspNetUsers");
            migrationBuilder.DropTable("Rank");
            migrationBuilder.DropTable("Image");
            migrationBuilder.DropTable("Rating");
            migrationBuilder.DropTable("RankBracket");
            migrationBuilder.DropTable("Season");
        }
    }
}
