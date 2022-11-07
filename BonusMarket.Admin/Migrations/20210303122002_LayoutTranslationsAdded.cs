using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BonusMarket.Admin.Migrations
{
    public partial class LayoutTranslationsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LayoutItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DomainName = table.Column<string>(nullable: true),
                    Twitter = table.Column<string>(nullable: true),
                    BookShopUrl = table.Column<string>(nullable: true),
                    Instagram = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    CategoryImage = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutItems", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LayoutItemTranslations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LayoutItemID = table.Column<int>(nullable: true),
                    MainTitle = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    FooterName = table.Column<string>(nullable: true),
                    LogoImage = table.Column<string>(nullable: true),
                    LogoShortImage = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutItemTranslations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LayoutItemTranslations_LayoutItems_LayoutItemID",
                        column: x => x.LayoutItemID,
                        principalTable: "LayoutItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LayoutItemTranslations_LayoutItemID",
                table: "LayoutItemTranslations",
                column: "LayoutItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LayoutItemTranslations");

            migrationBuilder.DropTable(
                name: "LayoutItems");
        }
    }
}
