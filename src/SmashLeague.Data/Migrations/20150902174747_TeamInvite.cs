using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SmashLeagueDataMigrations
{
    public partial class TeamInvite : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.DropTable("TeamPotentialPlayer");
            migration.CreateTable(
                name: "TeamInvite",
                columns: table => new
                {
                    TeamId = table.Column(type: "int", nullable: false),
                    PlayerId = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamInvite", x => new { x.TeamId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_TeamInvite_Player_PlayerId",
                        columns: x => x.PlayerId,
                        referencedTable: "Player",
                        referencedColumn: "PlayerId");
                    table.ForeignKey(
                        name: "FK_TeamInvite_Team_TeamId",
                        columns: x => x.TeamId,
                        referencedTable: "Teams",
                        referencedColumn: "TeamId");
                });
            migration.AddUniqueConstraint(
                name: "AK_Team_Name",
                table: "Teams",
                column: "Name");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropUniqueConstraint(name: "AK_Team_Name", table: "Teams");
            migration.DropTable("TeamInvite");
            migration.CreateTable(
                name: "TeamPotentialPlayer",
                columns: table => new
                {
                    PlayerId = table.Column(type: "int", nullable: false),
                    TeamId = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPotentialPlayer", x => new { x.PlayerId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TeamPotentialPlayer_Player_PlayerId",
                        columns: x => x.PlayerId,
                        referencedTable: "Player",
                        referencedColumn: "PlayerId");
                    table.ForeignKey(
                        name: "FK_TeamPotentialPlayer_Team_TeamId",
                        columns: x => x.TeamId,
                        referencedTable: "Teams",
                        referencedColumn: "TeamId");
                });
        }
    }
}
