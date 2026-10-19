using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPlatform.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddProductAndVariantForeignKeysToBundleItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "BundleItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VariantId",
                table: "BundleItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BundleItems_ProductId",
                table: "BundleItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleItems_VariantId",
                table: "BundleItems",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleItems_ProductVariant_VariantId",
                table: "BundleItems",
                column: "VariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleItems_Tbl_Products_ProductId",
                table: "BundleItems",
                column: "ProductId",
                principalTable: "Tbl_Products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BundleItems_ProductVariant_VariantId",
                table: "BundleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BundleItems_Tbl_Products_ProductId",
                table: "BundleItems");

            migrationBuilder.DropIndex(
                name: "IX_BundleItems_ProductId",
                table: "BundleItems");

            migrationBuilder.DropIndex(
                name: "IX_BundleItems_VariantId",
                table: "BundleItems");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "BundleItems");

            migrationBuilder.DropColumn(
                name: "VariantId",
                table: "BundleItems");
        }
    }
}
