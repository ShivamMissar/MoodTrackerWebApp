using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodTracker.Migrations
{
    /// <inheritdoc />
    public partial class RemovedMoodsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_moodEntries_mood_MoodId",
                table: "moodEntries");

            migrationBuilder.DropTable(
                name: "mood");

            migrationBuilder.DropIndex(
                name: "IX_moodEntries_MoodId",
                table: "moodEntries");

            migrationBuilder.DropColumn(
                name: "MoodId",
                table: "moodEntries");

            migrationBuilder.AddColumn<string>(
                name: "MoodColour",
                table: "moodEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MoodDescription",
                table: "moodEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MoodName",
                table: "moodEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoodColour",
                table: "moodEntries");

            migrationBuilder.DropColumn(
                name: "MoodDescription",
                table: "moodEntries");

            migrationBuilder.DropColumn(
                name: "MoodName",
                table: "moodEntries");

            migrationBuilder.AddColumn<int>(
                name: "MoodId",
                table: "moodEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "mood",
                columns: table => new
                {
                    MoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoodColour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoodDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoodName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mood", x => x.MoodId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_moodEntries_MoodId",
                table: "moodEntries",
                column: "MoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_moodEntries_mood_MoodId",
                table: "moodEntries",
                column: "MoodId",
                principalTable: "mood",
                principalColumn: "MoodId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
