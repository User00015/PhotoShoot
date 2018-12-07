using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoGallery.Migrations
{
    public partial class FixingEventsWhyHaveIDoneThis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate_DateId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EndDate_Day",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EndDate_Month",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EndDate_Year",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartDate_DateId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartDate_Day",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartDate_Month",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartDate_Year",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EndTime_Hour",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EndTime_Minute",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EndTime_TimeId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartTime_Hour",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartTime_Minute",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartTime_TimeId",
                table: "Events");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EndDate_DateId",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndDate_Day",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndDate_Month",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndDate_Year",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartDate_DateId",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartDate_Day",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartDate_Month",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartDate_Year",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndTime_Hour",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndTime_Minute",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndTime_TimeId",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartTime_Hour",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartTime_Minute",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartTime_TimeId",
                table: "Events",
                nullable: false,
                defaultValue: 0);
        }
    }
}
