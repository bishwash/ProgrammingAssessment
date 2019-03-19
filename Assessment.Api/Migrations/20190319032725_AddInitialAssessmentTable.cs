using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Assessment.Api.Migrations
{
    public partial class AddInitialAssessmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DOB = table.Column<DateTime>(type: "date", nullable: true),
                    FirstName = table.Column<string>(maxLength: 256, nullable: false),
                    GPA = table.Column<decimal>(type: "decimal(3, 2)", nullable: false),
                    LastName = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
