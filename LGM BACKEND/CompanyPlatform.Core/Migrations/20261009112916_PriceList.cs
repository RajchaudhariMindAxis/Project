using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPlatform.Core.Migrations
{
    /// <inheritdoc />
    public partial class PriceList : Migration
    {
protected override void Up(MigrationBuilder migrationBuilder)
        {
            // PriceLists is already created by AddPriceListTable.
            // Do not create the same table again.
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Intentionally empty.
            // Do not drop PriceLists because AddPriceListTable owns it.
        }

    }
}
