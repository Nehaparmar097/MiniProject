using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonationApp.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipients_UserID",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Donors_UserID",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "DateOfRegistration",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_UserID",
                table: "Recipients",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_UserID",
                table: "Donors",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipients_UserID",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Donors_UserID",
                table: "Donors");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfRegistration",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_UserID",
                table: "Recipients",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donors_UserID",
                table: "Donors",
                column: "UserID",
                unique: true);
        }
    }
}
