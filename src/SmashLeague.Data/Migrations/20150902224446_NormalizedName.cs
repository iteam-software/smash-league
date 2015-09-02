using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SmashLeagueDataMigrations
{
    public partial class NormalizedName : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_Team_Season_SeasonSeasonId", table: "Teams");
            migration.DropColumn(name: "SeasonSeasonId", table: "Teams");
            migration.AddColumn(
                name: "NormalizedName",
                table: "Teams",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
            migration.AddUniqueConstraint(
                name: "AK_Team_NormalizedName",
                table: "Teams",
                column: "NormalizedName");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropUniqueConstraint(name: "AK_Team_NormalizedName", table: "Teams");
            migration.DropColumn(name: "NormalizedName", table: "Teams");
            migration.AddColumn(
                name: "SeasonSeasonId",
                table: "Teams",
                type: "int",
                nullable: true);
            migration.AddForeignKey(
                name: "FK_Team_Season_SeasonSeasonId",
                table: "Teams",
                column: "SeasonSeasonId",
                referencedTable: "Season",
                referencedColumn: "SeasonId");
        }
    }
}
