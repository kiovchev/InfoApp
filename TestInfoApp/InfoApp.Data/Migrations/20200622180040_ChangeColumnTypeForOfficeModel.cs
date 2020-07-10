using Microsoft.EntityFrameworkCore.Migrations;

namespace InfoApp.Data.Migrations
{
    public partial class ChangeColumnTypeForOfficeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OfficeName",
                table: "Offices",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OfficeName",
                table: "Offices",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
