using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addmissing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AppLookupDetail_DepartmentLookupId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_HospitalBranches_BranchId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specialties_SpecialtyId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_SystemUsers_UserId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_DepartmentLookupId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DepartmentLookupId",
                table: "Doctors");

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorProfileOid",
                table: "SystemUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "SpecialtyId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "NphiesProviderId",
                table: "Doctors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LicenseNumber",
                table: "Doctors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<decimal>(
                name: "ConsultationFee",
                table: "Doctors",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Doctors",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameAr",
                table: "Doctors",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameEn",
                table: "Doctors",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenderId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HospitalBranchOid",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNameAr",
                table: "Doctors",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNameEn",
                table: "Doctors",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "LicenseExpiryDate",
                table: "Doctors",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "LicenseIssueDate",
                table: "Doctors",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LicenseTypeId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleNameAr",
                table: "Doctors",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleNameEn",
                table: "Doctors",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Doctors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NphiesLicenseNumber",
                table: "Doctors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Doctors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SpecialtyOid",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubSpecialtyId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DoctorAttachment",
                columns: table => new
                {
                    Oid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachmentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_DoctorAttachment", x => x.Oid);
                    table.ForeignKey(
                        name: "FK_DoctorAttachment_AppLookupDetail_AttachmentTypeId",
                        column: x => x.AttachmentTypeId,
                        principalTable: "AppLookupDetail",
                        principalColumn: "Oid");
                    table.ForeignKey(
                        name: "FK_DoctorAttachment_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Oid");
                });

            migrationBuilder.CreateTable(
                name: "DoctorBranch",
                columns: table => new
                {
                    Oid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_DoctorBranch", x => x.Oid);
                    table.ForeignKey(
                        name: "FK_DoctorBranch_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Oid");
                    table.ForeignKey(
                        name: "FK_DoctorBranch_HospitalBranches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "HospitalBranches",
                        principalColumn: "Oid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_DoctorProfileOid",
                table: "SystemUsers",
                column: "DoctorProfileOid");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentId",
                table: "Doctors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_GenderId",
                table: "Doctors",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_HospitalBranchOid",
                table: "Doctors",
                column: "HospitalBranchOid");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_LicenseTypeId",
                table: "Doctors",
                column: "LicenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialtyOid",
                table: "Doctors",
                column: "SpecialtyOid");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SubSpecialtyId",
                table: "Doctors",
                column: "SubSpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAttachment_AttachmentTypeId",
                table: "DoctorAttachment",
                column: "AttachmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAttachment_DoctorId",
                table: "DoctorAttachment",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorBranch_BranchId",
                table: "DoctorBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorBranch_DoctorId",
                table: "DoctorBranch",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AppLookupDetail_DepartmentId",
                table: "Doctors",
                column: "DepartmentId",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AppLookupDetail_GenderId",
                table: "Doctors",
                column: "GenderId",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AppLookupDetail_LicenseTypeId",
                table: "Doctors",
                column: "LicenseTypeId",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AppLookupDetail_SubSpecialtyId",
                table: "Doctors",
                column: "SubSpecialtyId",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_HospitalBranches_BranchId",
                table: "Doctors",
                column: "BranchId",
                principalTable: "HospitalBranches",
                principalColumn: "Oid");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_HospitalBranches_HospitalBranchOid",
                table: "Doctors",
                column: "HospitalBranchOid",
                principalTable: "HospitalBranches",
                principalColumn: "Oid");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specialties_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Oid");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specialties_SpecialtyOid",
                table: "Doctors",
                column: "SpecialtyOid",
                principalTable: "Specialties",
                principalColumn: "Oid");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_SystemUsers_UserId",
                table: "Doctors",
                column: "UserId",
                principalTable: "SystemUsers",
                principalColumn: "Oid");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemUsers_Doctors_DoctorProfileOid",
                table: "SystemUsers",
                column: "DoctorProfileOid",
                principalTable: "Doctors",
                principalColumn: "Oid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AppLookupDetail_DepartmentId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AppLookupDetail_GenderId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AppLookupDetail_LicenseTypeId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AppLookupDetail_SubSpecialtyId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_HospitalBranches_BranchId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_HospitalBranches_HospitalBranchOid",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specialties_SpecialtyId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specialties_SpecialtyOid",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_SystemUsers_UserId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemUsers_Doctors_DoctorProfileOid",
                table: "SystemUsers");

            migrationBuilder.DropTable(
                name: "DoctorAttachment");

            migrationBuilder.DropTable(
                name: "DoctorBranch");

            migrationBuilder.DropIndex(
                name: "IX_SystemUsers_DoctorProfileOid",
                table: "SystemUsers");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_DepartmentId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_GenderId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_HospitalBranchOid",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_LicenseTypeId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_SpecialtyOid",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_SubSpecialtyId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DoctorProfileOid",
                table: "SystemUsers");

            migrationBuilder.DropColumn(
                name: "ConsultationFee",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "FirstNameAr",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "FirstNameEn",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "HospitalBranchOid",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LastNameAr",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LastNameEn",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LicenseExpiryDate",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LicenseIssueDate",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LicenseTypeId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "MiddleNameAr",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "MiddleNameEn",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "NphiesLicenseNumber",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "SpecialtyOid",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "SubSpecialtyId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "Doctors");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SpecialtyId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NphiesProviderId",
                table: "Doctors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LicenseNumber",
                table: "Doctors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentLookupId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentLookupId",
                table: "Doctors",
                column: "DepartmentLookupId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AppLookupDetail_DepartmentLookupId",
                table: "Doctors",
                column: "DepartmentLookupId",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_HospitalBranches_BranchId",
                table: "Doctors",
                column: "BranchId",
                principalTable: "HospitalBranches",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specialties_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_SystemUsers_UserId",
                table: "Doctors",
                column: "UserId",
                principalTable: "SystemUsers",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
