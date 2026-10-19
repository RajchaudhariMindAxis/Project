using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPlatform.Core.Migrations
{
    /// <inheritdoc />
    public partial class BackendCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Accessories_Tbl_ProductVariants_VariantId",
                table: "Tbl_Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Products_Tbl_ProductVariants_VariantId",
                table: "Tbl_Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tbl_ProductVariants",
                table: "Tbl_ProductVariants");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Warehouse");

            migrationBuilder.RenameTable(
                name: "Tbl_ProductVariants",
                newName: "ProductVariant");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Unit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "MSMENo",
                table: "tbl_Customer",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GSTIN",
                table: "tbl_Customer",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DocsAttachment",
                table: "tbl_Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerTAN",
                table: "tbl_Customer",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerShortName",
                table: "tbl_Customer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerPAN",
                table: "tbl_Customer",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "tbl_Customer",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerCode",
                table: "tbl_Customer",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CIN",
                table: "tbl_Customer",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress",
                table: "tbl_Customer",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "tbl_Customer",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerMobile",
                table: "tbl_Customer",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tbl_Customer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "tbl_Customer",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccessoriesName",
                table: "Tbl_Accessories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AccessoriesCode",
                table: "Tbl_Accessories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Tbl_Accessories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Tbl_Accessories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinStock",
                table: "Tbl_Accessories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TaxId",
                table: "Tbl_Accessories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TrackStock",
                table: "Tbl_Accessories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Tbl_Accessories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tax",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Location",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Brand",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Branch",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "VariantName",
                table: "ProductVariant",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ProductVariant",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Attributes",
                table: "ProductVariant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductVariant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VariantCode",
                table: "ProductVariant",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductAccessoryRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AccessoriesId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MandatoryOptional = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PricingType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DefaultPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAccessoryRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAccessoryRule_Tbl_Accessories_AccessoriesId",
                        column: x => x.AccessoriesId,
                        principalTable: "Tbl_Accessories",
                        principalColumn: "AccessoriesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAccessoryRule_Tbl_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Tbl_Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductBarcode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BarcodeType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    VariantId = table.Column<int>(type: "int", nullable: true),
                    AccessoryId = table.Column<int>(type: "int", nullable: true),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBarcode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductBarcode_ProductVariant_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductBarcode_Tbl_Accessories_AccessoryId",
                        column: x => x.AccessoryId,
                        principalTable: "Tbl_Accessories",
                        principalColumn: "AccessoriesId");
                    table.ForeignKey(
                        name: "FK_ProductBarcode_Tbl_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Tbl_Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    SupplierCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SupplierGSTIN = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    SupplierContact = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SupplierAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    PaymentTerms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierId);
                    table.ForeignKey(
                        name: "FK_Supplier_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Accessories_BrandId",
                table: "Tbl_Accessories",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Accessories_TaxId",
                table: "Tbl_Accessories",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Accessories_UnitId",
                table: "Tbl_Accessories",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_ProductId",
                table: "ProductVariant",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAccessoryRule_AccessoriesId",
                table: "ProductAccessoryRule",
                column: "AccessoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAccessoryRule_ProductId",
                table: "ProductAccessoryRule",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBarcode_AccessoryId",
                table: "ProductBarcode",
                column: "AccessoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBarcode_ProductId",
                table: "ProductBarcode",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBarcode_VariantId",
                table: "ProductBarcode",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_BranchId",
                table: "Supplier",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_Tbl_Products_ProductId",
                table: "ProductVariant",
                column: "ProductId",
                principalTable: "Tbl_Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Accessories_Brand_BrandId",
                table: "Tbl_Accessories",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Accessories_ProductVariant_VariantId",
                table: "Tbl_Accessories",
                column: "VariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Accessories_Tax_TaxId",
                table: "Tbl_Accessories",
                column: "TaxId",
                principalTable: "Tax",
                principalColumn: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Accessories_Unit_UnitId",
                table: "Tbl_Accessories",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Products_ProductVariant_VariantId",
                table: "Tbl_Products",
                column: "VariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_Tbl_Products_ProductId",
                table: "ProductVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Accessories_Brand_BrandId",
                table: "Tbl_Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Accessories_ProductVariant_VariantId",
                table: "Tbl_Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Accessories_Tax_TaxId",
                table: "Tbl_Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Accessories_Unit_UnitId",
                table: "Tbl_Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Products_ProductVariant_VariantId",
                table: "Tbl_Products");

            migrationBuilder.DropTable(
                name: "ProductAccessoryRule");

            migrationBuilder.DropTable(
                name: "ProductBarcode");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Accessories_BrandId",
                table: "Tbl_Accessories");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Accessories_TaxId",
                table: "Tbl_Accessories");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Accessories_UnitId",
                table: "Tbl_Accessories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariant_ProductId",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "BillingAddress",
                table: "tbl_Customer");

            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "tbl_Customer");

            migrationBuilder.DropColumn(
                name: "CustomerMobile",
                table: "tbl_Customer");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tbl_Customer");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "tbl_Customer");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Tbl_Accessories");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Tbl_Accessories");

            migrationBuilder.DropColumn(
                name: "MinStock",
                table: "Tbl_Accessories");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "Tbl_Accessories");

            migrationBuilder.DropColumn(
                name: "TrackStock",
                table: "Tbl_Accessories");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Tbl_Accessories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "Attributes",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "VariantCode",
                table: "ProductVariant");

            migrationBuilder.RenameTable(
                name: "ProductVariant",
                newName: "Tbl_ProductVariants");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Warehouse",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Warehouse",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Warehouse",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Warehouse",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Warehouse",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MSMENo",
                table: "tbl_Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GSTIN",
                table: "tbl_Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocsAttachment",
                table: "tbl_Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerTAN",
                table: "tbl_Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerShortName",
                table: "tbl_Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerPAN",
                table: "tbl_Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "tbl_Customer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerCode",
                table: "tbl_Customer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CIN",
                table: "tbl_Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccessoriesName",
                table: "Tbl_Accessories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "AccessoriesCode",
                table: "Tbl_Accessories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "VariantName",
                table: "Tbl_ProductVariants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tbl_ProductVariants",
                table: "Tbl_ProductVariants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Accessories_Tbl_ProductVariants_VariantId",
                table: "Tbl_Accessories",
                column: "VariantId",
                principalTable: "Tbl_ProductVariants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Products_Tbl_ProductVariants_VariantId",
                table: "Tbl_Products",
                column: "VariantId",
                principalTable: "Tbl_ProductVariants",
                principalColumn: "Id");
        }
    }
}
