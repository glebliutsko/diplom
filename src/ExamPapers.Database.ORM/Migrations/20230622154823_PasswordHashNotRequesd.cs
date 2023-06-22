using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamPapers.Database.ORM.Migrations
{
    /// <inheritdoc />
    public partial class PasswordHashNotRequesd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "users",
                type: "character varying(144)",
                maxLength: 144,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(144)",
                oldMaxLength: 144);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "users",
                type: "character varying(144)",
                maxLength: 144,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(144)",
                oldMaxLength: 144,
                oldNullable: true);
        }
    }
}
