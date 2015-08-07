using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SmashLeagueDataMigrations
{
    public partial class MatchesToTeamJoinTable : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_Match_Team_Team2TeamId", table: "Match");
            migration.DropColumn(name: "Team2TeamId", table: "Match");
            migration.CreateTable(
                name: "Matchup",
                columns: table => new
                {
                    MatchId = table.Column(type: "int", nullable: false),
                    TeamId = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matchup", x => new { x.MatchId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_Matchup_Match_MatchId",
                        columns: x => x.MatchId,
                        referencedTable: "Match",
                        referencedColumn: "MatchId");
                    table.ForeignKey(
                        name: "FK_Matchup_Team_TeamId",
                        columns: x => x.TeamId,
                        referencedTable: "Teams",
                        referencedColumn: "TeamId");
                });
            migration.AddColumn(
                name: "TeamTeamId",
                table: "Rank",
                type: "int",
                nullable: true);
            migration.AddForeignKey(
                name: "FK_Rank_Team_TeamTeamId",
                table: "Rank",
                column: "TeamTeamId",
                referencedTable: "Teams",
                referencedColumn: "TeamId");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_Rank_Team_TeamTeamId", table: "Rank");
            migration.DropColumn(name: "TeamTeamId", table: "Rank");
            migration.DropTable("Matchup");
            migration.AddColumn(
                name: "Team2TeamId",
                table: "Match",
                type: "int",
                nullable: true);
            migration.AddForeignKey(
                name: "FK_Match_Team_Team2TeamId",
                table: "Match",
                column: "Team2TeamId",
                referencedTable: "Teams",
                referencedColumn: "TeamId");
        }
    }
}
