using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamPapers.Database.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AddDistributionTest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_distributionTests_testingSessions_TestingSessionId",
                table: "distributionTests");

            migrationBuilder.DropForeignKey(
                name: "FK_distributionTests_tests_TestId",
                table: "distributionTests");

            migrationBuilder.DropIndex(
                name: "IX_distributionTests_TestingSessionId",
                table: "distributionTests");

            migrationBuilder.DropColumn(
                name: "TestingSessionId",
                table: "distributionTests");

            migrationBuilder.RenameColumn(
                name: "TestId",
                table: "distributionTests",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_distributionTests_TestId",
                table: "distributionTests",
                newName: "IX_distributionTests_SessionId");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "testingSessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_testingSessions_TestId",
                table: "testingSessions",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_distributionTests_testingSessions_SessionId",
                table: "distributionTests",
                column: "SessionId",
                principalTable: "testingSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_testingSessions_tests_TestId",
                table: "testingSessions",
                column: "TestId",
                principalTable: "tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_distributionTests_testingSessions_SessionId",
                table: "distributionTests");

            migrationBuilder.DropForeignKey(
                name: "FK_testingSessions_tests_TestId",
                table: "testingSessions");

            migrationBuilder.DropIndex(
                name: "IX_testingSessions_TestId",
                table: "testingSessions");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "testingSessions");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "distributionTests",
                newName: "TestId");

            migrationBuilder.RenameIndex(
                name: "IX_distributionTests_SessionId",
                table: "distributionTests",
                newName: "IX_distributionTests_TestId");

            migrationBuilder.AddColumn<int>(
                name: "TestingSessionId",
                table: "distributionTests",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_distributionTests_TestingSessionId",
                table: "distributionTests",
                column: "TestingSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_distributionTests_testingSessions_TestingSessionId",
                table: "distributionTests",
                column: "TestingSessionId",
                principalTable: "testingSessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_distributionTests_tests_TestId",
                table: "distributionTests",
                column: "TestId",
                principalTable: "tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
