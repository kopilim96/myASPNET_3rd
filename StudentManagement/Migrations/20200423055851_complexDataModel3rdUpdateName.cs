using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagement.Migrations
{
    public partial class complexDataModel3rdUpdateName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Student_studentId",
                table: "Enrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "sudentId",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "studentId",
                table: "Student",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "studentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Student_studentId",
                table: "Enrollment",
                column: "studentId",
                principalTable: "Student",
                principalColumn: "studentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Student_studentId",
                table: "Enrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "studentId",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "sudentId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "sudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Student_studentId",
                table: "Enrollment",
                column: "studentId",
                principalTable: "Student",
                principalColumn: "sudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
