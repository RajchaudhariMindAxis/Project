using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPlatform.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyForeignKeyToPriceList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // AccessoriesImage is preserved from initial creation in 20261008085500_Warehouse.

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "PriceLists",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_Id",
                table: "PriceLists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceLists_Tbl_Currencies_Id",
                table: "PriceLists",
                column: "Id",
                principalTable: "Tbl_Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceLists_Tbl_Currencies_Id",
                table: "PriceLists");

            migrationBuilder.DropIndex(
                name: "IX_PriceLists_Id",
                table: "PriceLists");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PriceLists",
                newName: "CurrencyId");
        }
    }
}
