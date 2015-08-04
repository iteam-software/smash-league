using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SmashLeagueDataMigrations
{
    public partial class SeasonDataStructures : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    UserId = table.Column(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Player_ApplicationUser_UserId",
                        columns: x => x.UserId,
                        referencedTable: "AspNetUsers",
                        referencedColumn: "Id");
                });
            migration.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    MatchMakingRating = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingId);
                });
            migration.CreateTable(
                name: "Season",
                columns: table => new
                {
                    SeasonId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    Name = table.Column(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.SeasonId);
                });
            migration.CreateTable(
                name: "RankBracket",
                columns: table => new
                {
                    RankingBracketId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    MaximumMMR = table.Column(type: "int", nullable: false),
                    MinimumMMR = table.Column(type: "int", nullable: false),
                    Name = table.Column(type: "nvarchar(max)", nullable: false),
                    SeasonSeasonId = table.Column(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankBracket", x => x.RankingBracketId);
                    table.ForeignKey(
                        name: "FK_RankBracket_Season_SeasonSeasonId",
                        columns: x => x.SeasonSeasonId,
                        referencedTable: "Season",
                        referencedColumn: "SeasonId");
                });
            migration.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    Name = table.Column(type: "nvarchar(50)", nullable: false),
                    SeasonSeasonId = table.Column(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Team_Season_SeasonSeasonId",
                        columns: x => x.SeasonSeasonId,
                        referencedTable: "Season",
                        referencedColumn: "SeasonId");
                });
            migration.CreateTable(
                name: "Rank",
                columns: table => new
                {
                    RankId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    PlayerPlayerId = table.Column(type: "int", nullable: true),
                    Position = table.Column(type: "int", nullable: false),
                    RankBracketRankingBracketId = table.Column(type: "int", nullable: true),
                    RatingRatingId = table.Column(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rank", x => x.RankId);
                    table.ForeignKey(
                        name: "FK_Rank_Player_PlayerPlayerId",
                        columns: x => x.PlayerPlayerId,
                        referencedTable: "Player",
                        referencedColumn: "PlayerId");
                    table.ForeignKey(
                        name: "FK_Rank_RankBracket_RankBracketRankingBracketId",
                        columns: x => x.RankBracketRankingBracketId,
                        referencedTable: "RankBracket",
                        referencedColumn: "RankingBracketId");
                    table.ForeignKey(
                        name: "FK_Rank_Rating_RatingRatingId",
                        columns: x => x.RatingRatingId,
                        referencedTable: "Rating",
                        referencedColumn: "RatingId");
                });
            migration.CreateTable(
                name: "Series",
                columns: table => new
                {
                    SeriesId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    MatchCount = table.Column(type: "int", nullable: false),
                    SeasonSeasonId = table.Column(type: "int", nullable: true),
                    WinnerTeamId = table.Column(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.SeriesId);
                    table.ForeignKey(
                        name: "FK_Series_Season_SeasonSeasonId",
                        columns: x => x.SeasonSeasonId,
                        referencedTable: "Season",
                        referencedColumn: "SeasonId");
                    table.ForeignKey(
                        name: "FK_Series_Team_WinnerTeamId",
                        columns: x => x.WinnerTeamId,
                        referencedTable: "Teams",
                        referencedColumn: "TeamId");
                });
            migration.CreateTable(
                name: "Match",
                columns: table => new
                {
                    MatchId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    SeasonSeasonId = table.Column(type: "int", nullable: true),
                    SeriesSeriesId = table.Column(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Match_Season_SeasonSeasonId",
                        columns: x => x.SeasonSeasonId,
                        referencedTable: "Season",
                        referencedColumn: "SeasonId");
                    table.ForeignKey(
                        name: "FK_Match_Series_SeriesSeriesId",
                        columns: x => x.SeriesSeriesId,
                        referencedTable: "Series",
                        referencedColumn: "SeriesId");
                });
            migration.CreateIndex(
                name: "IX_Team_Name",
                table: "Teams",
                column: "Name");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("Match");
            migration.DropTable("Rank");
            migration.DropTable("Series");
            migration.DropTable("Player");
            migration.DropTable("RankBracket");
            migration.DropTable("Rating");
            migration.DropTable("Teams");
            migration.DropTable("Season");
        }
    }
}
