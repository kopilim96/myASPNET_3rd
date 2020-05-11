using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagement.Migrations
{
    public partial class complexDataModel2nd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "budget",
                table: "Department",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Budget");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "budget",
                table: "Department",
                type: "Budget",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");
        }
    }
}
