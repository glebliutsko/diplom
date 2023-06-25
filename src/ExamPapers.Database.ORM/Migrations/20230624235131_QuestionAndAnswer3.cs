using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamPapers.Database.ORM.Migrations
{
    /// <inheritdoc />
    public partial class QuestionAndAnswer3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_questions_OwnerId",
                table: "questions",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_users_OwnerId",
                table: "questions",
                column: "OwnerId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_users_OwnerId",
                table: "questions");

            migrationBuilder.DropIndex(
                name: "IX_questions_OwnerId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "questions");
        }
    }
}
