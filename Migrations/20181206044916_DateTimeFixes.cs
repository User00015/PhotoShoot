using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoGallery.Migrations
{
    public partial class DateTimeFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Time",
                newName: "TimeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Date",
                newName: "DateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeId",
                table: "Time",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DateId",
                table: "Date",
                newName: "Id");
        }
    }
}
