using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExamPapers.Database.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AddDistributionTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "testingSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DistributionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testingSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "distributionTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    TestId = table.Column<int>(type: "integer", nullable: false),
                    TestingSessionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_distributionTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_distributionTests_testingSessions_TestingSessionId",
                        column: x => x.TestingSessionId,
                        principalTable: "testingSessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_distributionTests_tests_TestId",
                        column: x => x.TestId,
                        principalTable: "tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_distributionTests_users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_distributionTests_StudentId",
                table: "distributionTests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_distributionTests_TestId",
                table: "distributionTests",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_distributionTests_TestingSessionId",
                table: "distributionTests",
                column: "TestingSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "distributionTests");

            migrationBuilder.DropTable(
                name: "testingSessions");
        }
    }
}
