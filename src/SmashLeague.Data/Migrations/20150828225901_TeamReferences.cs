using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SmashLeagueDataMigrations
{
    public partial class TeamReferences : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_Series_Team_WinnerTeamId", table: "Series");
            migration.CreateTable(
                name: "TeamPlayer",
                columns: table => new
                {
                    PlayerId = table.Column(type: "int", nullable: false),
                    TeamId = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayer", x => new { x.PlayerId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Player_PlayerId",
                        columns: x => x.PlayerId,
                        referencedTable: "Player",
                        referencedColumn: "PlayerId");
                    table.ForeignKey(
                        name: "FK_TeamPlayer_Team_TeamId",
                        columns: x => x.TeamId,
                        referencedTable: "Teams",
                        referencedColumn: "TeamId");
                });
            migration.CreateTable(
                name: "TeamPotentialPlayer",
                columns: table => new
                {
                    PlayerId = table.Column(type: "int", nullable: false),
                    TeamId = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPotentialPlayer", x => new { x.PlayerId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TeamPotentialPlayer_Player_PlayerId",
                        columns: x => x.PlayerId,
                        referencedTable: "Player",
                        referencedColumn: "PlayerId");
                    table.ForeignKey(
                        name: "FK_TeamPotentialPlayer_Team_TeamId",
                        columns: x => x.TeamId,
                        referencedTable: "Teams",
                        referencedColumn: "TeamId");
                });
            migration.AddColumn(
                name: "WinnerPlayerId",
                table: "Series",
                type: "int",
                nullable: true);
            migration.AddColumn(
                name: "TeamPlayerId",
                table: "Rank",
                type: "int",
                nullable: true);
            migration.AddColumn(
                name: "TeamTeamId1",
                table: "Rank",
                type: "int",
                nullable: true);
            migration.AddColumn(
                name: "LookingForTeam",
                table: "Player",
                type: "bit",
                nullable: false,
                defaultValue: false);
            migration.AddColumn(
                name: "PreferredRole",
                table: "Player",
                type: "int",
                nullable: true);
            migration.AddColumn(
                name: "Tag",
                table: "Player",
                type: "nvarchar(max)",
                nullable: true);
            migration.AddForeignKey(
                name: "FK_Rank_TeamPlayer_TeamPlayerId_TeamTeamId1",
                table: "Rank",
                columns: new[] { "TeamPlayerId", "TeamTeamId1" },
                referencedTable: "TeamPlayer",
                referencedColumns: new[] { "PlayerId", "TeamId" });
            migration.AddForeignKey(
                name: "FK_Series_TeamPlayer_WinnerPlayerId_WinnerTeamId",
                table: "Series",
                columns: new[] { "WinnerPlayerId", "WinnerTeamId" },
                referencedTable: "TeamPlayer",
                referencedColumns: new[] { "PlayerId", "TeamId" });
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_Rank_TeamPlayer_TeamPlayerId_TeamTeamId1", table: "Rank");
            migration.DropForeignKey(name: "FK_Series_TeamPlayer_WinnerPlayerId_WinnerTeamId", table: "Series");
            migration.DropColumn(name: "WinnerPlayerId", table: "Series");
            migration.DropColumn(name: "TeamPlayerId", table: "Rank");
            migration.DropColumn(name: "TeamTeamId1", table: "Rank");
            migration.DropColumn(name: "LookingForTeam", table: "Player");
            migration.DropColumn(name: "PreferredRole", table: "Player");
            migration.DropColumn(name: "Tag", table: "Player");
            migration.DropTable("TeamPlayer");
            migration.DropTable("TeamPotentialPlayer");
            migration.AddForeignKey(
                name: "FK_Series_Team_WinnerTeamId",
                table: "Series",
                column: "WinnerTeamId",
                referencedTable: "Teams",
                referencedColumn: "TeamId");
        }
    }
}
