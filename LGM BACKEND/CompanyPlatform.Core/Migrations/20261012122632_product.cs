using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPlatform.Core.Migrations
{
    /// <inheritdoc />
    public partial class product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Tbl_Products");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Tbl_Products",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Tbl_Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "HSNCode",
                table: "Tbl_Products",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Tbl_Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Tbl_Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MRP",
                table: "Tbl_Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinStock",
                table: "Tbl_Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ReorderLevel",
                table: "Tbl_Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TaxId",
                table: "Tbl_Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TrackBatch",
                table: "Tbl_Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrackSerial",
                table: "Tbl_Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrackStock",
                table: "Tbl_Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Tbl_Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Products_BrandId",
                table: "Tbl_Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Products_TaxId",
                table: "Tbl_Products",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Products_UnitId",
                table: "Tbl_Products",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Products_Brand_BrandId",
                table: "Tbl_Products",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Products_Tax_TaxId",
                table: "Tbl_Products",
                column: "TaxId",
                principalTable: "Tax",
                principalColumn: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Products_Unit_UnitId",
                table: "Tbl_Products",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Products_Brand_BrandId",
                table: "Tbl_Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Products_Tax_TaxId",
                table: "Tbl_Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Products_Unit_UnitId",
                table: "Tbl_Products");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Products_BrandId",
                table: "Tbl_Products");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Products_TaxId",
                table: "Tbl_Products");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Products_UnitId",
                table: "Tbl_Products");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Tbl_Products");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tbl_Products");

            migrationBuilder.DropColumn(
                name: "MRP",
                table: "Tbl_Products");

            migrationBuilder.DropColumn(
                name: "MinStock",
                table: "Tbl_Products");

            migrationBuilder.DropColumn(
                name: "ReorderLevel",
                table: "Tbl_Products");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "Tbl_Products");

            migrationBuilder.DropColumn(
                name: "TrackBatch",
                table: "Tbl_Products");

            migrationBuilder.DropColumn(
                name: "TrackSerial",
                table: "Tbl_Products");

            migrationBuilder.DropColumn(
                name: "TrackStock",
                table: "Tbl_Products");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Tbl_Products");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Tbl_Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Tbl_Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "HSNCode",
                table: "Tbl_Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "Tbl_Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
