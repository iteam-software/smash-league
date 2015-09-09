using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace SmashLeague.Data.Migrations
{
    public partial class ImageHeaderToTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeaderImageImageId",
                table: "Teams",
                isNullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_Team_Image_HeaderImageImageId",
                table: "Teams",
                column: "HeaderImageImageId",
                principalTable: "Image",
                principalColumn: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Team_Image_HeaderImageImageId", table: "Teams");
            migrationBuilder.DropColumn(name: "HeaderImageImageId", table: "Teams");
        }
    }
}
