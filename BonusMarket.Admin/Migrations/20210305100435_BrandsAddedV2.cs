using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BonusMarket.Admin.Migrations
{
    public partial class BrandsAddedV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(
            //     name: "MainImage",
            //     table: "Products");

            // migrationBuilder.DropColumn(
            //     name: "ModificationDate",
            //     table: "Pictures");

            // migrationBuilder.CreateTable(
            //     name: "Brand",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         CreationDate = table.Column<DateTime>(nullable: true),
            //         Status = table.Column<bool>(nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Brand", x => x.Id);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "BrandTranslate",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         BrandId = table.Column<int>(nullable: false),
            //         SeoName = table.Column<string>(nullable: true),
            //         Name = table.Column<string>(nullable: true),
            //         Language = table.Column<string>(nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_BrandTranslate", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_BrandTranslate_Brand_BrandId",
            //             column: x => x.BrandId,
            //             principalTable: "Brand",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Restrict);
            //     });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandTranslate_BrandId",
                table: "BrandTranslate",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brand_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brand_BrandId",
                table: "Products");

            // migrationBuilder.DropTable(
            //     name: "BrandTranslate");
            //
            // migrationBuilder.DropTable(
            //     name: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandId",
                table: "Products");

            // migrationBuilder.AddColumn<string>(
            //     name: "MainImage",
            //     table: "Products",
            //     type: "nvarchar(max)",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<DateTime>(
            //     name: "ModificationDate",
            //     table: "Pictures",
            //     type: "datetime2",
            //     nullable: true);
        }
    }
}
