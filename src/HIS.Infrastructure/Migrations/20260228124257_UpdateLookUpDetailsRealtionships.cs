using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLookUpDetailsRealtionships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SlotDurationMinutes",
                table: "DoctorSchedules",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "DoctorScheduleExceptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "AppLookupDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AppLookupDetail_DayOfWeek",
                table: "AppLookupDetail",
                column: "DayOfWeek");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DayOfWeek",
                table: "DoctorSchedules",
                column: "DayOfWeek");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorScheduleExceptions_DayOfWeek",
                table: "DoctorScheduleExceptions",
                column: "DayOfWeek");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorScheduleExceptions_AppLookupDetail_DayOfWeek",
                table: "DoctorScheduleExceptions",
                column: "DayOfWeek",
                principalTable: "AppLookupDetail",
                principalColumn: "DayOfWeek");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_AppLookupDetail_DayOfWeek",
                table: "DoctorSchedules",
                column: "DayOfWeek",
                principalTable: "AppLookupDetail",
                principalColumn: "DayOfWeek");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorScheduleExceptions_AppLookupDetail_DayOfWeek",
                table: "DoctorScheduleExceptions");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_AppLookupDetail_DayOfWeek",
                table: "DoctorSchedules");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSchedules_DayOfWeek",
                table: "DoctorSchedules");

            migrationBuilder.DropIndex(
                name: "IX_DoctorScheduleExceptions_DayOfWeek",
                table: "DoctorScheduleExceptions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AppLookupDetail_DayOfWeek",
                table: "AppLookupDetail");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "DoctorScheduleExceptions");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "AppLookupDetail");

            migrationBuilder.AlterColumn<int>(
                name: "SlotDurationMinutes",
                table: "DoctorSchedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
