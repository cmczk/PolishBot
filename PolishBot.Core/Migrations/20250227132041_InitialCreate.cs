using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolishBot.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "flashcards",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    word = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    explanation = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    translation = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    example = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flashcards", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flashcards");
        }
    }
}
