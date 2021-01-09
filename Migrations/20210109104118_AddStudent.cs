using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XYZLaundry.Migrations
{
    public partial class AddStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(nullable: true),
                    NICNo = table.Column<string>(nullable: true),
                    DateofBirth = table.Column<DateTime>(nullable: false),
                    School = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    GuardianName = table.Column<string>(nullable: true),
                    GuardianContactNumber = table.Column<string>(nullable: true),
                    GuardianAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
