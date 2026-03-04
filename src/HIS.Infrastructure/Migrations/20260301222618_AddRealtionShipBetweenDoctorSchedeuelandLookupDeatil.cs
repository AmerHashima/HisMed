using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRealtionShipBetweenDoctorSchedeuelandLookupDeatil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "SlotDurationMinutes",
                table: "DoctorSchedules",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "DayOfWeek",
            //    table: "DoctorSchedules",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int"); 
           
            migrationBuilder.DropColumn(name: "DayOfWeek", table: "DoctorSchedules");
            migrationBuilder.AddColumn<Guid>(name: "DayOfWeek"
             , table: "DoctorSchedules",
               type: "uniqueidentifier",
              nullable: false,
              defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
              ); 
          

            migrationBuilder.AddColumn<Guid>(
                name: "DayOfWeek",
                table: "DoctorScheduleExceptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                principalColumn: "Oid",
                onDelete: ReferentialAction.NoAction);
           

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_AppLookupDetail_DayOfWeek",
                table: "DoctorSchedules",
                column: "DayOfWeek",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid",
                onDelete: ReferentialAction.NoAction);
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

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "DoctorScheduleExceptions");

            migrationBuilder.AlterColumn<int>(
                name: "SlotDurationMinutes",
                table: "DoctorSchedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
            migrationBuilder.DropColumn(name:"DayOfWeek",table: "DoctorSchedules");
            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "DoctorSchedules",
                type: "int",
                nullable: false,
                defaultValue:0
                
                ); 
            
        }
    }
}
