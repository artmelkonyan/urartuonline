using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BonusMarket.Admin.Migrations
{
    public partial class PicturesNewPathAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(
            //     name: "CreationDate",
            //     table: "Brand");

            migrationBuilder.AddColumn<string>(
                name: "NewPath",
                table: "Pictures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPath",
                table: "Pictures");

            // migrationBuilder.AddColumn<DateTime>(
            //     name: "CreationDate",
            //     table: "Brand",
            //     type: "datetime2",
            //     nullable: true);
        }
    }
}
