using Microsoft.EntityFrameworkCore.Migrations;

namespace ChoiceWebApp.Data.Migrations
{
    public partial class ChangeDiscColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplines_Teachers_TeacherId1",
                table: "Disciplines");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_TeacherId1",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "Disciplines");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Disciplines",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_TeacherId",
                table: "Disciplines",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplines_Teachers_TeacherId",
                table: "Disciplines",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplines_Teachers_TeacherId",
                table: "Disciplines");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_TeacherId",
                table: "Disciplines");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "Disciplines",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TeacherId1",
                table: "Disciplines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_TeacherId1",
                table: "Disciplines",
                column: "TeacherId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplines_Teachers_TeacherId1",
                table: "Disciplines",
                column: "TeacherId1",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
