using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPlatform.Core.Migrations
{
    /// <inheritdoc />
    public partial class Warehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyShortName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GSTIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencySymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 1.0m),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ProductVariants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_ProductVariants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HSNCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    VariantId = table.Column<int>(type: "int", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Tbl_Products_Tbl_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Tbl_Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tbl_Products_Tbl_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Tbl_Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tbl_Products_Tbl_ProductVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Tbl_ProductVariants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Products_CategoryId",
                table: "Tbl_Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Products_CurrencyId",
                table: "Tbl_Products",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Products_VariantId",
                table: "Tbl_Products",
                column: "VariantId");

            migrationBuilder.CreateTable(
                name: "Tbl_Accessories",
                columns: table => new
                {
                    AccessoriesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessoriesCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessoriesName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessoriesImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    VariantId = table.Column<int>(type: "int", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Accessories", x => x.AccessoriesId);
                    table.ForeignKey(
                        name: "FK_Tbl_Accessories_Tbl_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Tbl_Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tbl_Accessories_Tbl_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Tbl_Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tbl_Accessories_Tbl_ProductVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Tbl_ProductVariants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Accessories_CategoryId",
                table: "Tbl_Accessories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Accessories_CurrencyId",
                table: "Tbl_Accessories",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Accessories_VariantId",
                table: "Tbl_Accessories",
                column: "VariantId");

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchId);
                    table.ForeignKey(
                        name: "FK_Branch_Tbl_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Tbl_Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CompanyId",
                table: "Branch",
                column: "CompanyId");

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    WarehouseCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WarehouseName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    WarehouseType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.WarehouseId);
                    table.ForeignKey(
                        name: "FK_Warehouse_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_BranchId",
                table: "Warehouse",
                column: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "Tbl_Accessories");

            migrationBuilder.DropTable(
                name: "Tbl_Products");

            migrationBuilder.DropTable(
                name: "Tbl_ProductVariants");

            migrationBuilder.DropTable(
                name: "Tbl_Categories");

            migrationBuilder.DropTable(
                name: "Tbl_Currencies");

            migrationBuilder.DropTable(
                name: "Tbl_Companies");
        }
    }
}
