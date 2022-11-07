using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BonusMarket.Admin.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleNumber = table.Column<int>(nullable: false),
                    ModuleName = table.Column<string>(nullable: true),
                    PermissionNumber = table.Column<int>(nullable: false),
                    PermissionName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            // migrationBuilder.CreateTable(
            //     name: "Pictures",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         RealPath = table.Column<string>(nullable: true),
            //         RealName = table.Column<string>(nullable: true),
            //         SeoName = table.Column<string>(nullable: true),
            //         Main = table.Column<bool>(nullable: true),
            //         FullPath = table.Column<string>(nullable: true),
            //         CreationDate = table.Column<DateTime>(nullable: true),
            //         ModificationDate = table.Column<DateTime>(nullable: true),
            //         Status = table.Column<bool>(nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Pictures", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "Products",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Count = table.Column<int>(nullable: true),
            //         Price = table.Column<decimal>(nullable: true),
            //         OldPrice = table.Column<decimal>(nullable: true),
            //         Sku = table.Column<string>(nullable: true),
            //         ShowOnHomePage = table.Column<bool>(nullable: true),
            //         MainImage = table.Column<string>(nullable: true),
            //         Published = table.Column<bool>(nullable: true),
            //         BrandId = table.Column<int>(nullable: true),
            //         CreationDate = table.Column<DateTime>(nullable: true),
            //         ModificationDate = table.Column<DateTime>(nullable: true),
            //         Status = table.Column<bool>(nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Products", x => x.Id);
            //     });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    SystemName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SystemRole = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            // migrationBuilder.CreateTable(
            //     name: "Users",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         FirstName = table.Column<string>(nullable: true),
            //         LastName = table.Column<string>(nullable: true),
            //         Email = table.Column<string>(nullable: true),
            //         Phone = table.Column<string>(nullable: true),
            //         Password = table.Column<string>(nullable: true),
            //         Address = table.Column<string>(nullable: true),
            //         PasswordHash = table.Column<string>(nullable: true),
            //         CreationDate = table.Column<DateTime>(nullable: true),
            //         ModificationDate = table.Column<DateTime>(nullable: true),
            //         Status = table.Column<bool>(nullable: true),
            //         Role = table.Column<int>(nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Users", x => x.Id);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Product_To_Picture",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         ProductId = table.Column<int>(nullable: true),
            //         PictureId = table.Column<int>(nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Product_To_Picture", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_Product_To_Picture_Pictures_PictureId",
            //             column: x => x.PictureId,
            //             principalTable: "Pictures",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Restrict);
            //         table.ForeignKey(
            //             name: "FK_Product_To_Picture_Products_ProductId",
            //             column: x => x.ProductId,
            //             principalTable: "Products",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Restrict);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "ProductTranslation",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         ProductId = table.Column<int>(nullable: false),
            //         Language = table.Column<string>(nullable: true),
            //         Name = table.Column<string>(nullable: true),
            //         ShortDescription = table.Column<string>(nullable: true),
            //         FullDescription = table.Column<string>(nullable: true),
            //         CreationDate = table.Column<DateTime>(nullable: false),
            //         ModificationDate = table.Column<DateTime>(nullable: true),
            //         Status = table.Column<bool>(nullable: false),
            //         SeoName = table.Column<string>(nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_ProductTranslation", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_ProductTranslation_Products_ProductId",
            //             column: x => x.ProductId,
            //             principalTable: "Products",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Restrict);
            //     });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: true),
                    PermissionId = table.Column<int>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_To_Picture_PictureId",
                table: "Product_To_Picture",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_To_Picture_ProductId",
                table: "Product_To_Picture",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslation_ProductId",
                table: "ProductTranslation",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            // migrationBuilder.DropTable(
            //     name: "Product_To_Picture");
            //
            // migrationBuilder.DropTable(
            //     name: "ProductTranslation");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            // migrationBuilder.DropTable(
            //     name: "Pictures");

            // migrationBuilder.DropTable(
            //     name: "Products");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            // migrationBuilder.DropTable(
            //     name: "Users");
        }
    }
}
