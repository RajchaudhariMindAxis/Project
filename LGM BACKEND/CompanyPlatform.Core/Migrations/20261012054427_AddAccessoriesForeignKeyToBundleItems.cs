using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPlatform.Core.Migrations
{
    public partial class AddAccessoriesForeignKeyToBundleItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // AccessoriesId and FK_BundleItems_Tbl_Accessories_AccessoriesId
            // are configured in the immediately following migration:
            // 20261012060545_RemoveItemIdAndItemTypeFromBundleItems.
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Intentionally empty. Managed by 20261012060545_RemoveItemIdAndItemTypeFromBundleItems.
        }
    }
}