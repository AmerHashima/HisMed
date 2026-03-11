using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateIcd110TableAndRemovesPropsFromDignsis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiagnosisCode",
                table: "Diagnoses");

            migrationBuilder.DropColumn(
                name: "DiagnosisName",
                table: "Diagnoses");

            migrationBuilder.CreateTable(
                name: "emr_icd110",
                columns: table => new
                {
                    Oid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    CodeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dagger = table.Column<int>(type: "int", nullable: false),
                    Asterisk = table.Column<int>(type: "int", nullable: false),
                    Valid = table.Column<int>(type: "int", nullable: false),
                    AustCode = table.Column<int>(type: "int", nullable: false),
                    AsciidDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AsciiShortDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Effectivefrom = table.Column<DateOnly>(type: "date", nullable: false),
                    Inactive = table.Column<DateOnly>(type: "date", nullable: true),
                    reactivated = table.Column<DateOnly>(type: "date", nullable: true),
                    Sex = table.Column<int>(type: "int", nullable: true),
                    Stype = table.Column<int>(type: "int", nullable: true),
                    AgeL = table.Column<int>(type: "int", nullable: true),
                    AgeH = table.Column<int>(type: "int", nullable: true),
                    Rdiag = table.Column<int>(type: "int", nullable: false),
                    MorphCode = table.Column<int>(type: "int", nullable: false),
                    ConceptChange = table.Column<DateOnly>(type: "date", nullable: true),
                    UnacceptPdx = table.Column<int>(type: "int", nullable: false),
                    Atype = table.Column<int>(type: "int", nullable: true),
                    DiagnosisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_emr_icd110", x => x.Oid);
                    table.ForeignKey(
                        name: "FK_emr_icd110_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Oid",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_emr_icd110_DiagnosisId",
                table: "emr_icd110",
                column: "DiagnosisId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emr_icd110");

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisCode",
                table: "Diagnoses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisName",
                table: "Diagnoses",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
