using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeDignosisFkNullale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_emr_icd110_Diagnoses_DiagnosisId",
                table: "emr_icd110");

            migrationBuilder.AlterColumn<Guid>(
                name: "DiagnosisId",
                table: "emr_icd110",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_emr_icd110_Diagnoses_DiagnosisId",
                table: "emr_icd110",
                column: "DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Oid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_emr_icd110_Diagnoses_DiagnosisId",
                table: "emr_icd110");

            migrationBuilder.AlterColumn<Guid>(
                name: "DiagnosisId",
                table: "emr_icd110",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_emr_icd110_Diagnoses_DiagnosisId",
                table: "emr_icd110",
                column: "DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
