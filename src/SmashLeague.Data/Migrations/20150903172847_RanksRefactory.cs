using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;
using SmashLeague.Data;

namespace SmashLeagueDataMigrations
{
    public partial class RanksRefactory : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_Rank_Player_PlayerPlayerId", table: "Rank");
            migration.DropForeignKey(name: "FK_Rank_Team_TeamTeamId", table: "Rank");
            migration.DropForeignKey(name: "FK_Rank_TeamPlayer_TeamPlayerId_TeamTeamId1", table: "Rank");
            migration.DropColumn(name: "PlayerPlayerId", table: "Rank");
            migration.DropColumn(name: "TeamPlayerId", table: "Rank");
            migration.DropColumn(name: "TeamTeamId", table: "Rank");
            migration.DropColumn(name: "TeamTeamId1", table: "Rank");
            migration.AddColumn(
                name: "RankRankId",
                table: "Teams",
                type: "int",
                nullable: true);
            migration.AddColumn(
                name: "Type",
                table: "RankBracket",
                type: "int",
                nullable: false,
                defaultValue: 1);
            migration.AddColumn(
                name: "RankRankId",
                table: "Player",
                type: "int",
                nullable: true);
            migration.AddForeignKey(
                name: "FK_Player_Rank_RankRankId",
                table: "Player",
                column: "RankRankId",
                referencedTable: "Rank",
                referencedColumn: "RankId");
            migration.AddForeignKey(
                name: "FK_Team_Rank_RankRankId",
                table: "Teams",
                column: "RankRankId",
                referencedTable: "Rank",
                referencedColumn: "RankId");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_Player_Rank_RankRankId", table: "Player");
            migration.DropForeignKey(name: "FK_Team_Rank_RankRankId", table: "Teams");
            migration.DropColumn(name: "RankRankId", table: "Teams");
            migration.DropColumn(name: "Type", table: "RankBracket");
            migration.DropColumn(name: "RankRankId", table: "Player");
            migration.AddColumn(
                name: "PlayerPlayerId",
                table: "Rank",
                type: "int",
                nullable: true);
            migration.AddColumn(
                name: "TeamPlayerId",
                table: "Rank",
                type: "int",
                nullable: true);
            migration.AddColumn(
                name: "TeamTeamId",
                table: "Rank",
                type: "int",
                nullable: true);
            migration.AddColumn(
                name: "TeamTeamId1",
                table: "Rank",
                type: "int",
                nullable: true);
            migration.AddForeignKey(
                name: "FK_Rank_Player_PlayerPlayerId",
                table: "Rank",
                column: "PlayerPlayerId",
                referencedTable: "Player",
                referencedColumn: "PlayerId");
            migration.AddForeignKey(
                name: "FK_Rank_Team_TeamTeamId",
                table: "Rank",
                column: "TeamTeamId",
                referencedTable: "Teams",
                referencedColumn: "TeamId");
            migration.AddForeignKey(
                name: "FK_Rank_TeamPlayer_TeamPlayerId_TeamTeamId1",
                table: "Rank",
                columns: new[] { "TeamPlayerId", "TeamTeamId1" },
                referencedTable: "TeamPlayer",
                referencedColumns: new[] { "PlayerId", "TeamId" });
        }
    }
}
