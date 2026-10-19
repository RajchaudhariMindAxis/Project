
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPlatform.Core.Migrations
{
    public partial class AddTax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add soft-delete flag without deleting existing Warehouse data.
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Warehouse",
                type: "bit",
                nullable: false,
                defaultValue: false);

            // Rename Unit columns while preserving existing values.
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Unit",
                newName: "UnitName");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Unit",
                newName: "UnitCode");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Unit",
                newName: "IsActive");

            // Rename Warehouse column.
            migrationBuilder.RenameColumn(
                name: "WarehouseType",
                table: "Warehouse",
                newName: "Type");

            // Rename Brand columns while preserving existing values.
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Brand",
                newName: "BrandCode");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Brand",
                newName: "BrandName");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Brand",
                newName: "IsActive");

            // Create Tax table.
            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    TaxId = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    TaxCode = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false),

                    TaxName = table.Column<string>(
                        type: "nvarchar(150)",
                        maxLength: 150,
                        nullable: false),

                    Rate = table.Column<decimal>(
                        type: "decimal(18,2)",
                        nullable: false),

                    TaxType = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
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
                        "PK_Tax",
                        x => x.TaxId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the table created by this migration.
            migrationBuilder.DropTable(
                name: "Tax");

            // Reverse Brand column renames.
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Brand",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "BrandName",
                table: "Brand",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BrandCode",
                table: "Brand",
                newName: "Code");

            // Reverse Unit column renames.
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Unit",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "UnitCode",
                table: "Unit",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "UnitName",
                table: "Unit",
                newName: "Name");

            // Reverse Warehouse column rename.
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Warehouse",
                newName: "WarehouseType");

            // Remove the flag added by this migration.
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Warehouse");
        }
    }
}
