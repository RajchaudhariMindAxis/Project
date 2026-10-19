using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPlatform.Core.Migrations
{
    public partial class AddAccessoriesForeignKeyToBundleItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_BundleItems_Accessories_AccessoriesId",
                table: "BundleItems",
                column: "AccessoriesId",
                principalTable: "Accessories",
                principalColumn: "AccessoriesId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BundleItems_Accessories_AccessoriesId",
                table: "BundleItems");

            migrationBuilder.DropIndex(
                name: "IX_BundleItems_AccessoriesId",
                table: "BundleItems");

            migrationBuilder.DropColumn(
                name: "AccessoriesId",
                table: "BundleItems");
        }
    }
}