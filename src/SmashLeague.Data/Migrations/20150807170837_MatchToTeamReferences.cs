using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SmashLeagueDataMigrations
{
    public partial class MatchToTeamReferences : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.AddColumn(
                name: "Team2TeamId",
                table: "Match",
                type: "int",
                nullable: true);
            migration.AddColumn(
                name: "WinnerTeamId",
                table: "Match",
                type: "int",
                nullable: true);
            migration.AddForeignKey(
                name: "FK_Match_Team_Team2TeamId",
                table: "Match",
                column: "Team2TeamId",
                referencedTable: "Teams",
                referencedColumn: "TeamId");
            migration.AddForeignKey(
                name: "FK_Match_Team_WinnerTeamId",
                table: "Match",
                column: "WinnerTeamId",
                referencedTable: "Teams",
                referencedColumn: "TeamId");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_Match_Team_Team2TeamId", table: "Match");
            migration.DropForeignKey(name: "FK_Match_Team_WinnerTeamId", table: "Match");
            migration.DropColumn(name: "Team2TeamId", table: "Match");
            migration.DropColumn(name: "WinnerTeamId", table: "Match");
        }
    }
}
