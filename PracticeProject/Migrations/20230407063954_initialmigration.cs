using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeProject.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Citizenshipnumber = table.Column<int>(type: "int", nullable: false),
                    Fathersname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mothersname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hostel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emptyseats = table.Column<int>(type: "int", nullable: false),
                    Totalseats = table.Column<int>(type: "int", nullable: false),
                    PAN = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hostel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Hostel_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hostel_PAN",
                table: "Hostel",
                column: "PAN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hostel_StudentID",
                table: "Hostel",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Citizenshipnumber",
                table: "Student",
                column: "Citizenshipnumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hostel");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
