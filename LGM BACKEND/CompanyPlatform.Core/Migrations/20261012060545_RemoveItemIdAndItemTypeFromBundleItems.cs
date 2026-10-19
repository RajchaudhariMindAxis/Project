using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPlatform.Core.Migrations
{
    /// <inheritdoc />
    public partial class RemoveItemIdAndItemTypeFromBundleItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "BundleItems");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "BundleItems");

            migrationBuilder.AddColumn<int>(
                name: "AccessoriesId",
                table: "BundleItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BundleItems_AccessoriesId",
                table: "BundleItems",
                column: "AccessoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleItems_Tbl_Accessories_AccessoriesId",
                table: "BundleItems",
                column: "AccessoriesId",
                principalTable: "Tbl_Accessories",
                principalColumn: "AccessoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BundleItems_Tbl_Accessories_AccessoriesId",
                table: "BundleItems");

            migrationBuilder.DropIndex(
                name: "IX_BundleItems_AccessoriesId",
                table: "BundleItems");

            migrationBuilder.DropColumn(
                name: "AccessoriesId",
                table: "BundleItems");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "BundleItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "BundleItems",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
