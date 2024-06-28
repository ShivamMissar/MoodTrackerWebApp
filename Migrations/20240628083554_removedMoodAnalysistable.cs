using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodTracker.Migrations
{
    /// <inheritdoc />
    public partial class removedMoodAnalysistable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "moodAnalysis");

            migrationBuilder.AddColumn<string>(
                name: "mood_insight",
                table: "moodEntries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "recommendation",
                table: "moodEntries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mood_insight",
                table: "moodEntries");

            migrationBuilder.DropColumn(
                name: "recommendation",
                table: "moodEntries");

            migrationBuilder.CreateTable(
                name: "moodAnalysis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mood_insight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recommendation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moodAnalysis", x => x.Id);
                });
        }
    }
}
