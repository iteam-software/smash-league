using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SmashLeagueDataMigrations
{
    public partial class TeamImage : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.AddColumn(
                name: "TeamImageProfileImageId",
                table: "Teams",
                type: "int",
                nullable: true);
            migration.AddForeignKey(
                name: "FK_Team_Image_TeamImageProfileImageId",
                table: "Teams",
                column: "TeamImageProfileImageId",
                referencedTable: "Image",
                referencedColumn: "ProfileImageId");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_Team_Image_TeamImageProfileImageId", table: "Teams");
            migration.DropColumn(name: "TeamImageProfileImageId", table: "Teams");
        }
    }
}
