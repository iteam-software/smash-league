using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SmashLeagueDataMigrations
{
    public partial class TeamOwners : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "TeamOwner",
                columns: table => new
                {
                    TeamId = table.Column(type: "int", nullable: false),
                    PlayerId = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamOwner", x => new { x.TeamId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_TeamOwner_Player_PlayerId",
                        columns: x => x.PlayerId,
                        referencedTable: "Player",
                        referencedColumn: "PlayerId");
                    table.ForeignKey(
                        name: "FK_TeamOwner_Team_TeamId",
                        columns: x => x.TeamId,
                        referencedTable: "Teams",
                        referencedColumn: "TeamId");
                });
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("TeamOwner");
        }
    }
}
