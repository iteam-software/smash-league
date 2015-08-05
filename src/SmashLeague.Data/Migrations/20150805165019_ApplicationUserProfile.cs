using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SmashLeagueDataMigrations
{
    public partial class ApplicationUserProfile : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.AddColumn(
                name: "Birthday",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
            migration.AddColumn(
                name: "First",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
            migration.AddColumn(
                name: "HeaderImage",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
            migration.AddColumn(
                name: "Last",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
            migration.AddColumn(
                name: "Location",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
            migration.AddColumn(
                name: "ProfileImage",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropColumn(name: "Birthday", table: "AspNetUsers");
            migration.DropColumn(name: "First", table: "AspNetUsers");
            migration.DropColumn(name: "HeaderImage", table: "AspNetUsers");
            migration.DropColumn(name: "Last", table: "AspNetUsers");
            migration.DropColumn(name: "Location", table: "AspNetUsers");
            migration.DropColumn(name: "ProfileImage", table: "AspNetUsers");
        }
    }
}
