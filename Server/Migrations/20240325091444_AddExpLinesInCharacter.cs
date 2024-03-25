using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddExpLinesInCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Exp",
                table: "Characters",
                newName: "ToLevelExp");

            migrationBuilder.AddColumn<int>(
                name: "CurrentEXP",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentEXP",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "ToLevelExp",
                table: "Characters",
                newName: "Exp");
        }
    }
}
