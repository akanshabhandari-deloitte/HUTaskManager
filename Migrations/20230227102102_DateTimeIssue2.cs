using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseApi.Migrations
{
    public partial class DateTimeIssue2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Issues",
                newName: "Updated_At");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Issues",
                newName: "Created_At");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated_At",
                table: "Issues",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "Issues",
                newName: "CreatedDate");
        }
    }
}
