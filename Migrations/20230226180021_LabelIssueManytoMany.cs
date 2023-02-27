using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseApi.Migrations
{
    public partial class LabelIssueManytoMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Issues_issueId",
                table: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_Labels_issueId",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "issueId",
                table: "Labels");

            migrationBuilder.CreateTable(
                name: "IssueLabel",
                columns: table => new
                {
                    LabelId = table.Column<int>(type: "int", nullable: false),
                    issueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueLabel", x => new { x.LabelId, x.issueId });
                    table.ForeignKey(
                        name: "FK_IssueLabel_Issues_issueId",
                        column: x => x.issueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueLabel_Labels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLabel_issueId",
                table: "IssueLabel",
                column: "issueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueLabel");

            migrationBuilder.AddColumn<int>(
                name: "issueId",
                table: "Labels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Labels_issueId",
                table: "Labels",
                column: "issueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Issues_issueId",
                table: "Labels",
                column: "issueId",
                principalTable: "Issues",
                principalColumn: "Id");
        }
    }
}
