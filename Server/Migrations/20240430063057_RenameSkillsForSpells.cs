using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class RenameSkillsForSpells : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Skill5",
                table: "Characters",
                newName: "Spell5");

            migrationBuilder.RenameColumn(
                name: "Skill4",
                table: "Characters",
                newName: "Spell4");

            migrationBuilder.RenameColumn(
                name: "Skill3",
                table: "Characters",
                newName: "Spell3");

            migrationBuilder.RenameColumn(
                name: "Skill2",
                table: "Characters",
                newName: "Spell2");

            migrationBuilder.RenameColumn(
                name: "Skill1",
                table: "Characters",
                newName: "Spell1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Spell5",
                table: "Characters",
                newName: "Skill5");

            migrationBuilder.RenameColumn(
                name: "Spell4",
                table: "Characters",
                newName: "Skill4");

            migrationBuilder.RenameColumn(
                name: "Spell3",
                table: "Characters",
                newName: "Skill3");

            migrationBuilder.RenameColumn(
                name: "Spell2",
                table: "Characters",
                newName: "Skill2");

            migrationBuilder.RenameColumn(
                name: "Spell1",
                table: "Characters",
                newName: "Skill1");
        }
    }
}
