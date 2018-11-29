using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoGallery.Migrations
{
    public partial class UpdatedEventModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EndDateId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndTimeId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartTimeId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimePerSlot",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Time",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hour = table.Column<int>(nullable: false),
                    Minute = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Time", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EndDateId",
                table: "Events",
                column: "EndDateId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EndTimeId",
                table: "Events",
                column: "EndTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_StartTimeId",
                table: "Events",
                column: "StartTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Date_EndDateId",
                table: "Events",
                column: "EndDateId",
                principalTable: "Date",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Time_EndTimeId",
                table: "Events",
                column: "EndTimeId",
                principalTable: "Time",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Time_StartTimeId",
                table: "Events",
                column: "StartTimeId",
                principalTable: "Time",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Date_EndDateId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Time_EndTimeId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Time_StartTimeId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Time");

            migrationBuilder.DropIndex(
                name: "IX_Events_EndDateId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_EndTimeId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_StartTimeId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EndDateId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EndTimeId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartTimeId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TimePerSlot",
                table: "Events");
        }
    }
}
