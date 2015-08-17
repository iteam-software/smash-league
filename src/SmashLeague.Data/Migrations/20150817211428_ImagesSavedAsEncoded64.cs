using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SmashLeagueDataMigrations
{
    public partial class ImagesSavedAsEncoded64 : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.DropColumn(name: "Bytes", table: "Image");
            migration.CreateTable(
                name: "DefaultImages",
                columns: table => new
                {
                    Name = table.Column(type: "nvarchar(450)", nullable: false),
                    ImageProfileImageId = table.Column(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultImages", x => x.Name);
                    table.ForeignKey(
                        name: "FK_DefaultImages_Image_ImageProfileImageId",
                        columns: x => x.ImageProfileImageId,
                        referencedTable: "Image",
                        referencedColumn: "ProfileImageId");
                });
            migration.AddColumn(
                name: "Data",
                table: "Image",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropColumn(name: "Data", table: "Image");
            migration.DropTable("DefaultImages");
            migration.AddColumn(
                name: "Bytes",
                table: "Image",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
