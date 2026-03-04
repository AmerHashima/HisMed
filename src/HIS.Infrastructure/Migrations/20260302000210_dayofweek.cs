using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dayofweek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_AppLookupDetail_DayOfWeek",
                table: "DoctorSchedules");

            migrationBuilder.RenameColumn(
                name: "DayOfWeek",
                table: "DoctorSchedules",
                newName: "DayOfWeekId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSchedules_DayOfWeek",
                table: "DoctorSchedules",
                newName: "IX_DoctorSchedules_DayOfWeekId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_AppLookupDetail_DayOfWeekId",
                table: "DoctorSchedules",
                column: "DayOfWeekId",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_AppLookupDetail_DayOfWeekId",
                table: "DoctorSchedules");

            migrationBuilder.RenameColumn(
                name: "DayOfWeekId",
                table: "DoctorSchedules",
                newName: "DayOfWeek");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSchedules_DayOfWeekId",
                table: "DoctorSchedules",
                newName: "IX_DoctorSchedules_DayOfWeek");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_AppLookupDetail_DayOfWeek",
                table: "DoctorSchedules",
                column: "DayOfWeek",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
