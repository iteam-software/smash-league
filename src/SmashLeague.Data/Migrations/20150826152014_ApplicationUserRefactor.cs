using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SmashLeagueDataMigrations
{
    public partial class ApplicationUserRefactor : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.DropColumn(name: "First", table: "AspNetUsers");
            migration.DropColumn(name: "Last", table: "AspNetUsers");
            migration.AddColumn(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropColumn(name: "Name", table: "AspNetUsers");
            migration.AddColumn(
                name: "First",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
            migration.AddColumn(
                name: "Last",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
