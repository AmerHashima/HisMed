using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LinkDoctorScheduelExceptionToAppLookUpDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorScheduleExceptions_AppLookupDetail_DayOfWeek",
                table: "DoctorScheduleExceptions");

            migrationBuilder.DropIndex(
                name: "IX_DoctorScheduleExceptions_DayOfWeek",
                table: "DoctorScheduleExceptions");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "DoctorScheduleExceptions");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorScheduleExceptions_DayOfWeekId",
                table: "DoctorScheduleExceptions",
                column: "DayOfWeekId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorScheduleExceptions_AppLookupDetail_DayOfWeekId",
                table: "DoctorScheduleExceptions",
                column: "DayOfWeekId",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid",
                onDelete: ReferentialAction.NoAction);   ///
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorScheduleExceptions_AppLookupDetail_DayOfWeekId",
                table: "DoctorScheduleExceptions");

            migrationBuilder.DropIndex(
                name: "IX_DoctorScheduleExceptions_DayOfWeekId",
                table: "DoctorScheduleExceptions");

            migrationBuilder.AddColumn<Guid>(
                name: "DayOfWeek",
                table: "DoctorScheduleExceptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DoctorScheduleExceptions_DayOfWeek",
                table: "DoctorScheduleExceptions",
                column: "DayOfWeek");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorScheduleExceptions_AppLookupDetail_DayOfWeek",
                table: "DoctorScheduleExceptions",
                column: "DayOfWeek",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
