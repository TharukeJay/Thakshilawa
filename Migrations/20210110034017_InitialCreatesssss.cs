using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Thakshilawa.Migrations
{
    public partial class InitialCreatesssss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Classes");

            migrationBuilder.AddColumn<int>(
                name: "CourseDuration",
                table: "Classes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyFee",
                table: "Classes",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StudentCapacity",
                table: "Classes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubjectName",
                table: "Classes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseDuration",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "MonthlyFee",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "StudentCapacity",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "SubjectName",
                table: "Classes");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Classes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Classes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Classes",
                type: "datetime2",
                nullable: true);
        }
    }
}
