using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SmashLeagueDataMigrations
{
    public partial class ApplicationUserAndImage : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.DropColumn(name: "HeaderImage", table: "AspNetUsers");
            migration.DropColumn(name: "ProfileImage", table: "AspNetUsers");
            migration.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ProfileImageId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    Bytes = table.Column(type: "varbinary(max)", nullable: true),
                    MimeType = table.Column(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ProfileImageId);
                });
            migration.AddColumn(
                name: "Battletag",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
            migration.AddColumn(
                name: "HeaderImageProfileImageId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
            migration.AddColumn(
                name: "ProfileImageProfileImageId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
            migration.AddForeignKey(
                name: "FK_ApplicationUser_Image_HeaderImageProfileImageId",
                table: "AspNetUsers",
                column: "HeaderImageProfileImageId",
                referencedTable: "Image",
                referencedColumn: "ProfileImageId");
            migration.AddForeignKey(
                name: "FK_ApplicationUser_Image_ProfileImageProfileImageId",
                table: "AspNetUsers",
                column: "ProfileImageProfileImageId",
                referencedTable: "Image",
                referencedColumn: "ProfileImageId");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_ApplicationUser_Image_HeaderImageProfileImageId", table: "AspNetUsers");
            migration.DropForeignKey(name: "FK_ApplicationUser_Image_ProfileImageProfileImageId", table: "AspNetUsers");
            migration.DropColumn(name: "Battletag", table: "AspNetUsers");
            migration.DropColumn(name: "HeaderImageProfileImageId", table: "AspNetUsers");
            migration.DropColumn(name: "ProfileImageProfileImageId", table: "AspNetUsers");
            migration.DropTable("Image");
            migration.AddColumn(
                name: "HeaderImage",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
            migration.AddColumn(
                name: "ProfileImage",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
