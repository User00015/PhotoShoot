using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoGallery.Migrations
{
    public partial class ComplexObjectsInEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Date_EndDateId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Time_EndTimeId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Date_StartDateId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Time_StartTimeId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Time",
                newName: "TimeId");

            migrationBuilder.RenameColumn(
                name: "StartTimeId",
                table: "Events",
                newName: "StartTimeTimeId");

            migrationBuilder.RenameColumn(
                name: "StartDateId",
                table: "Events",
                newName: "StartDateDateId");

            migrationBuilder.RenameColumn(
                name: "EndTimeId",
                table: "Events",
                newName: "EndTimeTimeId");

            migrationBuilder.RenameColumn(
                name: "EndDateId",
                table: "Events",
                newName: "EndDateDateId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_StartTimeId",
                table: "Events",
                newName: "IX_Events_StartTimeTimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_StartDateId",
                table: "Events",
                newName: "IX_Events_StartDateDateId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_EndTimeId",
                table: "Events",
                newName: "IX_Events_EndTimeTimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_EndDateId",
                table: "Events",
                newName: "IX_Events_EndDateDateId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Date",
                newName: "DateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Date_EndDateDateId",
                table: "Events",
                column: "EndDateDateId",
                principalTable: "Date",
                principalColumn: "DateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Time_EndTimeTimeId",
                table: "Events",
                column: "EndTimeTimeId",
                principalTable: "Time",
                principalColumn: "TimeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Date_StartDateDateId",
                table: "Events",
                column: "StartDateDateId",
                principalTable: "Date",
                principalColumn: "DateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Time_StartTimeTimeId",
                table: "Events",
                column: "StartTimeTimeId",
                principalTable: "Time",
                principalColumn: "TimeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Date_EndDateDateId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Time_EndTimeTimeId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Date_StartDateDateId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Time_StartTimeTimeId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "TimeId",
                table: "Time",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "StartTimeTimeId",
                table: "Events",
                newName: "StartTimeId");

            migrationBuilder.RenameColumn(
                name: "StartDateDateId",
                table: "Events",
                newName: "StartDateId");

            migrationBuilder.RenameColumn(
                name: "EndTimeTimeId",
                table: "Events",
                newName: "EndTimeId");

            migrationBuilder.RenameColumn(
                name: "EndDateDateId",
                table: "Events",
                newName: "EndDateId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_StartTimeTimeId",
                table: "Events",
                newName: "IX_Events_StartTimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_StartDateDateId",
                table: "Events",
                newName: "IX_Events_StartDateId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_EndTimeTimeId",
                table: "Events",
                newName: "IX_Events_EndTimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_EndDateDateId",
                table: "Events",
                newName: "IX_Events_EndDateId");

            migrationBuilder.RenameColumn(
                name: "DateId",
                table: "Date",
                newName: "Id");

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
                name: "FK_Events_Date_StartDateId",
                table: "Events",
                column: "StartDateId",
                principalTable: "Date",
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
    }
}
