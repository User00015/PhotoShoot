using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoGallery.Migrations
{
    public partial class ApptIsOpen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "Appointment",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "Appointment");
        }
    }
}
