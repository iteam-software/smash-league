using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SmashLeagueDataMigrations
{
    public partial class RemoveMimeType : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.DropColumn(name: "MimeType", table: "Image");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.AddColumn(
                name: "MimeType",
                table: "Image",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
