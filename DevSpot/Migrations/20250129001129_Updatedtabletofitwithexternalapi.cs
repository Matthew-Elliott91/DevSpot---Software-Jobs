using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevSpot.Migrations
{
    /// <inheritdoc />
    public partial class Updatedtabletofitwithexternalapi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostedDate",
                table: "JobPostings",
                newName: "DatePosted");

            migrationBuilder.AddColumn<string>(
                name: "CompanyDomain",
                table: "JobPostings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CompanyObjectId",
                table: "JobPostings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Hybrid",
                table: "JobPostings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "JobUrl",
                table: "JobPostings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "JobPostings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Remote",
                table: "JobPostings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SalaryString",
                table: "JobPostings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "JobPostings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRecruitingAgency = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_CompanyObjectId",
                table: "JobPostings",
                column: "CompanyObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostings_Company_CompanyObjectId",
                table: "JobPostings",
                column: "CompanyObjectId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPostings_Company_CompanyObjectId",
                table: "JobPostings");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropIndex(
                name: "IX_JobPostings_CompanyObjectId",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "CompanyDomain",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "CompanyObjectId",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "Hybrid",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "JobUrl",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "Remote",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "SalaryString",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "JobPostings");

            migrationBuilder.RenameColumn(
                name: "DatePosted",
                table: "JobPostings",
                newName: "PostedDate");
        }
    }
}
