
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPlatform.Core.Migrations
{
    public partial class AddPriceListTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop existing foreign keys before renaming the category table.
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Accessories_Tbl_Categories_CategoryId",
                table: "Tbl_Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Products_Tbl_Categories_CategoryId",
                table: "Tbl_Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tbl_Categories",
                table: "Tbl_Categories");

            // Rename the existing table without deleting its data.
            migrationBuilder.RenameTable(
                name: "Tbl_Categories",
                newName: "Category");

            // Rename the existing primary-key column.
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Category",
                newName: "CategoryId");

            // Preserve IsDeleted and add IsActive as a separate flag.
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: true);

            // Match the Category model's maximum length.
            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Category",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Category",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryId");

            // Self-referencing category relationship.
            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId");

            // Restore product and accessory relationships to the renamed table.
            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Accessories_Category_CategoryId",
                table: "Tbl_Accessories",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Products_Category_CategoryId",
                table: "Tbl_Products",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId");

            // Create the PriceLists table.
            migrationBuilder.CreateTable(
                name: "PriceLists",
                columns: table => new
                {
                    PriceListId = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    Code = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false),

                    Name = table.Column<string>(
                        type: "nvarchar(150)",
                        maxLength: 150,
                        nullable: false),

                    CustomerChannelType = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false),

                    CurrencyId = table.Column<int>(
                        type: "int",
                        nullable: false),

                    EffectiveFrom = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false),

                    EffectiveTo = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: true),

                    IsActive = table.Column<bool>(
                        type: "bit",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_PriceLists",
                        x => x.PriceListId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove relationships introduced by this migration.
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Accessories_Category_CategoryId",
                table: "Tbl_Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Products_Category_CategoryId",
                table: "Tbl_Products");

            migrationBuilder.DropIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category");

            // Remove the table introduced by this migration.
            migrationBuilder.DropTable(
                name: "PriceLists");

            // Remove only the columns added by this migration.
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            // Restore the original table and primary-key names.
            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Category",
                newName: "Id");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Tbl_Categories");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Tbl_Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tbl_Categories",
                table: "Tbl_Categories",
                column: "Id");

            // Restore the original foreign keys.
            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Accessories_Tbl_Categories_CategoryId",
                table: "Tbl_Accessories",
                column: "CategoryId",
                principalTable: "Tbl_Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Products_Tbl_Categories_CategoryId",
                table: "Tbl_Products",
                column: "CategoryId",
                principalTable: "Tbl_Categories",
                principalColumn: "Id");
        }
    }
}
