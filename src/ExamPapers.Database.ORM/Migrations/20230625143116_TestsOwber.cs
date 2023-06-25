using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamPapers.Database.ORM.Migrations
{
    /// <inheritdoc />
    public partial class TestsOwber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "tests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tests_OwnerId",
                table: "tests",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_tests_users_OwnerId",
                table: "tests",
                column: "OwnerId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tests_users_OwnerId",
                table: "tests");

            migrationBuilder.DropIndex(
                name: "IX_tests_OwnerId",
                table: "tests");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "tests");
        }
    }
}
