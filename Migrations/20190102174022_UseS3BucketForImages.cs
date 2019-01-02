using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoGallery.Migrations
{
    public partial class UseS3BucketForImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Images",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Images");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Images",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
