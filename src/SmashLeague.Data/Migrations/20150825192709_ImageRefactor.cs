using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SmashLeagueDataMigrations
{
    public partial class ImageRefactor : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.DropColumn(name: "Data", table: "Image");
            migration.AddColumn(
                name: "Source",
                table: "Image",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropColumn(name: "Source", table: "Image");
            migration.AddColumn(
                name: "Data",
                table: "Image",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
