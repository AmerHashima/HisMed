using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStartAndEndDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "DoctorSchedules",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "DoctorSchedules",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "DoctorSchedules");
        }
    }
}
