using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class docorscedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_AppLookupDetail_DayOfWeekId",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "SlotDurationMinutes",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "DoctorSchedules");

            migrationBuilder.RenameColumn(
                name: "DayOfWeekId",
                table: "DoctorSchedules",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSchedules_DayOfWeekId",
                table: "DoctorSchedules",
                newName: "IX_DoctorSchedules_StatusId");

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "DoctorSchedules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsPriority",
                table: "DoctorSchedules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "SpecialtyId",
                table: "DoctorSchedules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DoctorScheduleDetail",
                columns: table => new
                {
                    Oid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    SlotDurationMinutes = table.Column<float>(type: "real", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorScheduleDetail", x => x.Oid);
                    table.ForeignKey(
                        name: "FK_DoctorScheduleDetail_AppLookupDetail_DayOfWeekId",
                        column: x => x.DayOfWeekId,
                        principalTable: "AppLookupDetail",
                        principalColumn: "Oid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorScheduleDetail_DoctorSchedules_MasterId",
                        column: x => x.MasterId,
                        principalTable: "DoctorSchedules",
                        principalColumn: "Oid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_BranchId",
                table: "DoctorSchedules",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_SpecialtyId",
                table: "DoctorSchedules",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorScheduleDetail_DayOfWeekId",
                table: "DoctorScheduleDetail",
                column: "DayOfWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorScheduleDetail_MasterId",
                table: "DoctorScheduleDetail",
                column: "MasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_AppLookupDetail_StatusId",
                table: "DoctorSchedules",
                column: "StatusId",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_HospitalBranches_BranchId",
                table: "DoctorSchedules",
                column: "BranchId",
                principalTable: "HospitalBranches",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_Specialties_SpecialtyId",
                table: "DoctorSchedules",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_AppLookupDetail_StatusId",
                table: "DoctorSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_HospitalBranches_BranchId",
                table: "DoctorSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_Specialties_SpecialtyId",
                table: "DoctorSchedules");

            migrationBuilder.DropTable(
                name: "DoctorScheduleDetail");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSchedules_BranchId",
                table: "DoctorSchedules");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSchedules_SpecialtyId",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "IsPriority",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "DoctorSchedules");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "DoctorSchedules",
                newName: "DayOfWeekId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSchedules_StatusId",
                table: "DoctorSchedules",
                newName: "IX_DoctorSchedules_DayOfWeekId");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndTime",
                table: "DoctorSchedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<float>(
                name: "SlotDurationMinutes",
                table: "DoctorSchedules",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartTime",
                table: "DoctorSchedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_AppLookupDetail_DayOfWeekId",
                table: "DoctorSchedules",
                column: "DayOfWeekId",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
