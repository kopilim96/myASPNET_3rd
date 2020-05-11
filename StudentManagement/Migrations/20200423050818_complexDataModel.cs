using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace StudentManagement.Migrations
{
	public partial class complexDataModel : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "lastName",
				table: "Student",
				maxLength: 20,
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(20)",
				oldMaxLength: 20,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "firstName",
				table: "Student",
				maxLength: 30,
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(20)",
				oldMaxLength: 20,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "courseTitle",
				table: "Course",
				maxLength: 50,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)",
				oldNullable: true);

			//migrationBuilder.AddColumn<int>(
			//    name: "departmentId",
			//    table: "Course",
			//    nullable: false,
			//    defaultValue: 0);

			migrationBuilder.CreateTable(
				name: "Instructor",
				columns: table => new
				{
					instructorId = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					firstName = table.Column<string>(maxLength: 30, nullable: false),
					lastName = table.Column<string>(maxLength: 30, nullable: false),
					hireDate = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Instructor", x => x.instructorId);
				});

			migrationBuilder.CreateTable(
				name: "CourseAssignment",
				columns: table => new
				{
					instructorId = table.Column<int>(nullable: false),
					courseId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CourseAssignment", x => new { x.courseId, x.instructorId });
					table.ForeignKey(
						name: "FK_CourseAssignment_Course_courseId",
						column: x => x.courseId,
						principalTable: "Course",
						principalColumn: "courseId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_CourseAssignment_Instructor_instructorId",
						column: x => x.instructorId,
						principalTable: "Instructor",
						principalColumn: "instructorId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Department",
				columns: table => new
				{
					departmentId = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					departName = table.Column<string>(maxLength: 40, nullable: true),
					budget = table.Column<decimal>(type: "money", nullable: false),
					departStartDate = table.Column<DateTime>(nullable: false),
					instructorId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Department", x => x.departmentId);
					table.ForeignKey(
						name: "FK_Department_Instructor_instructorId",
						column: x => x.instructorId,
						principalTable: "Instructor",
						principalColumn: "instructorId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.Sql("Insert into dbo.Department (departName, budget, departStartDate) values ('Temp', 0.00, Getdate())");

			migrationBuilder.AddColumn<int>(
				name: "departmentId",
				table: "Course",
				nullable: false,
				defaultValue: 1
				);

			migrationBuilder.CreateTable(
				name: "OfficeAssignment",
				columns: table => new
				{
					instructorId = table.Column<int>(nullable: false),
					location = table.Column<string>(maxLength: 30, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_OfficeAssignment", x => x.instructorId);
					table.ForeignKey(
						name: "FK_OfficeAssignment_Instructor_instructorId",
						column: x => x.instructorId,
						principalTable: "Instructor",
						principalColumn: "instructorId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Course_departmentId",
				table: "Course",
				column: "departmentId");

			migrationBuilder.CreateIndex(
				name: "IX_CourseAssignment_instructorId",
				table: "CourseAssignment",
				column: "instructorId");

			migrationBuilder.CreateIndex(
				name: "IX_Department_instructorId",
				table: "Department",
				column: "instructorId");

			migrationBuilder.AddForeignKey(
				name: "FK_Course_Department_departmentId",
				table: "Course",
				column: "departmentId",
				principalTable: "Department",
				principalColumn: "departmentId",
				onDelete: ReferentialAction.Cascade);

		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Course_Department_departmentId",
				table: "Course");

			migrationBuilder.DropTable(
				name: "CourseAssignment");

			migrationBuilder.DropTable(
				name: "Department");

			migrationBuilder.DropTable(
				name: "OfficeAssignment");

			migrationBuilder.DropTable(
				name: "Instructor");

			migrationBuilder.DropIndex(
				name: "IX_Course_departmentId",
				table: "Course");

			migrationBuilder.DropColumn(
				name: "departmentId",
				table: "Course");

			migrationBuilder.AlterColumn<string>(
				name: "lastName",
				table: "Student",
				type: "nvarchar(20)",
				maxLength: 20,
				nullable: true,
				oldClrType: typeof(string),
				oldMaxLength: 20);

			migrationBuilder.AlterColumn<string>(
				name: "firstName",
				table: "Student",
				type: "nvarchar(20)",
				maxLength: 20,
				nullable: true,
				oldClrType: typeof(string),
				oldMaxLength: 30);

			migrationBuilder.AlterColumn<string>(
				name: "courseTitle",
				table: "Course",
				type: "nvarchar(max)",
				nullable: true,
				oldClrType: typeof(string),
				oldMaxLength: 50,
				oldNullable: true);
		}
	}
}
