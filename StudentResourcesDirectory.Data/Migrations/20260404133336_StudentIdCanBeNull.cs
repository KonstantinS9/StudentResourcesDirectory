using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentResourcesDirectory.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentIdCanBeNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Students_StudentId",
                table: "Resources");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Resources",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Students_StudentId",
                table: "Resources",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Students_StudentId",
                table: "Resources");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Resources",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Students_StudentId",
                table: "Resources",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
