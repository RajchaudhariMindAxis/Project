using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPlatform.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentSeriesBranchForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DocumentSeries_BranchId",
                table: "DocumentSeries",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentSeries_Branch_BranchId",
                table: "DocumentSeries",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentSeries_Branch_BranchId",
                table: "DocumentSeries");

            migrationBuilder.DropIndex(
                name: "IX_DocumentSeries_BranchId",
                table: "DocumentSeries");
        }
    }
}
