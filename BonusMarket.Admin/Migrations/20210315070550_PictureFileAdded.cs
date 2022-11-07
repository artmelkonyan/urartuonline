using Microsoft.EntityFrameworkCore.Migrations;

namespace BonusMarket.Admin.Migrations
{
    public partial class PictureFileAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Pictures",
                nullable: true);


            migrationBuilder.CreateIndex(
                name: "IX_Pictures_FileId",
                table: "Pictures",
                column: "FileId");


            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Files_FileId",
                table: "Pictures",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Files_FileId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_FileId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Pictures");

        }
    }
}
