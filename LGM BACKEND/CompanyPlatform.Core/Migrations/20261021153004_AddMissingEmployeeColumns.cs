using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPlatform.Core.Migrations
{
    public partial class AddMissingEmployeeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Level Master table if it does not exist.
            migrationBuilder.CreateTable(
                name: "tbl_LevelMasters",
                columns: table => new
                {
                    ID = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    LevelName = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    LevelOrder = table.Column<int>(
                        type: "int",
                        nullable: false),

                    IsActive = table.Column<bool>(
                        type: "bit",
                        nullable: false),

                    IsDeleted = table.Column<bool>(
                        type: "bit",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_LevelMasters", x => x.ID);
                });

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "tbl_Employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleCalendarId",
                table: "tbl_Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LevelID",
                table: "tbl_Employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "tbl_Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Employee_CompanyId",
                table: "tbl_Employee",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Employee_LevelID",
                table: "tbl_Employee",
                column: "LevelID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Employee_Tbl_Companies_CompanyId",
                table: "tbl_Employee",
                column: "CompanyId",
                principalTable: "Tbl_Companies",
                principalColumn: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Employee_tbl_LevelMasters_LevelID",
                table: "tbl_Employee",
                column: "LevelID",
                principalTable: "tbl_LevelMasters",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Employee_Tbl_Companies_CompanyId",
                table: "tbl_Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Employee_tbl_LevelMasters_LevelID",
                table: "tbl_Employee");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Employee_CompanyId",
                table: "tbl_Employee");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Employee_LevelID",
                table: "tbl_Employee");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "tbl_Employee");

            migrationBuilder.DropColumn(
                name: "GoogleCalendarId",
                table: "tbl_Employee");

            migrationBuilder.DropColumn(
                name: "LevelID",
                table: "tbl_Employee");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "tbl_Employee");

            migrationBuilder.DropTable(
                name: "tbl_LevelMasters");
        }
    }
}