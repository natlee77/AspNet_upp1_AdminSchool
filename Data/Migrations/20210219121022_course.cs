using Microsoft.EntityFrameworkCore.Migrations;

namespace Upp1_admin.Data.Migrations
{
    public partial class course : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SchoolCourseId",
                table: "SchoolClasses",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SchoolCourses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolCourses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClasses_SchoolCourseId",
                table: "SchoolClasses",
                column: "SchoolCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClasses_SchoolCourses_SchoolCourseId",
                table: "SchoolClasses",
                column: "SchoolCourseId",
                principalTable: "SchoolCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClasses_SchoolCourses_SchoolCourseId",
                table: "SchoolClasses");

            migrationBuilder.DropTable(
                name: "SchoolCourses");

            migrationBuilder.DropIndex(
                name: "IX_SchoolClasses_SchoolCourseId",
                table: "SchoolClasses");

            migrationBuilder.DropColumn(
                name: "SchoolCourseId",
                table: "SchoolClasses");
        }
    }
}
