using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addPatientData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppLookupMaster",
                columns: table => new
                {
                    Oid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    LookupCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LookupNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LookupNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsSystem = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
                    table.PrimaryKey("PK_AppLookupMaster", x => x.Oid);
                    table.ForeignKey(
                        name: "FK_AppLookupMaster_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Oid");
                    table.ForeignKey(
                        name: "FK_AppLookupMaster_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "Users",
                        principalColumn: "Oid");
                    table.ForeignKey(
                        name: "FK_AppLookupMaster_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "Oid");
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    MRN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NationalID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdentifierType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false, computedColumnSql: "([FirstNameAr] + ' ' + ISNULL([MiddleNameAr] + ' ', '') + [LastNameAr])", stored: false),
                    FullNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false, computedColumnSql: "([FirstNameEn] + ' ' + ISNULL([MiddleNameEn] + ' ', '') + [LastNameEn])", stored: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmergencyName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    EmergencyRelation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmergencyMobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientID);
                    table.ForeignKey(
                        name: "FK_Patients_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Oid");
                    table.ForeignKey(
                        name: "FK_Patients_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "Users",
                        principalColumn: "Oid");
                    table.ForeignKey(
                        name: "FK_Patients_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "Oid");
                });

            migrationBuilder.CreateTable(
                name: "AppLookupDetail",
                columns: table => new
                {
                    Oid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    LookupMasterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValueCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValueNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ValueNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
                    table.PrimaryKey("PK_AppLookupDetail", x => x.Oid);
                    table.ForeignKey(
                        name: "FK_AppLookupDetail_Master",
                        column: x => x.LookupMasterID,
                        principalTable: "AppLookupMaster",
                        principalColumn: "Oid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppLookupDetail_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Oid");
                    table.ForeignKey(
                        name: "FK_AppLookupDetail_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "Users",
                        principalColumn: "Oid");
                    table.ForeignKey(
                        name: "FK_AppLookupDetail_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "Oid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppLookupDetail_CreatedBy",
                table: "AppLookupDetail",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AppLookupDetail_DeletedBy",
                table: "AppLookupDetail",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AppLookupDetail_IsDefault",
                table: "AppLookupDetail",
                column: "IsDefault");

            migrationBuilder.CreateIndex(
                name: "IX_AppLookupDetail_MasterID",
                table: "AppLookupDetail",
                column: "LookupMasterID");

            migrationBuilder.CreateIndex(
                name: "IX_AppLookupDetail_SortOrder",
                table: "AppLookupDetail",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_AppLookupDetail_UpdatedBy",
                table: "AppLookupDetail",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "UQ_LookupDetail",
                table: "AppLookupDetail",
                columns: new[] { "LookupMasterID", "ValueCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppLookupMaster_CreatedBy",
                table: "AppLookupMaster",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AppLookupMaster_DeletedBy",
                table: "AppLookupMaster",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AppLookupMaster_IsSystem",
                table: "AppLookupMaster",
                column: "IsSystem");

            migrationBuilder.CreateIndex(
                name: "IX_AppLookupMaster_LookupCode",
                table: "AppLookupMaster",
                column: "LookupCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppLookupMaster_LookupNameEn",
                table: "AppLookupMaster",
                column: "LookupNameEn");

            migrationBuilder.CreateIndex(
                name: "IX_AppLookupMaster_UpdatedBy",
                table: "AppLookupMaster",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_BloodGroup",
                table: "Patients",
                column: "BloodGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CreatedBy",
                table: "Patients",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DeletedBy",
                table: "Patients",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Email",
                table: "Patients",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Gender",
                table: "Patients",
                column: "Gender");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_IsActive",
                table: "Patients",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Mobile",
                table: "Patients",
                column: "Mobile");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MRN",
                table: "Patients",
                column: "MRN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_NameAr",
                table: "Patients",
                columns: new[] { "LastNameAr", "FirstNameAr" });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_NameEn",
                table: "Patients",
                columns: new[] { "LastNameEn", "FirstNameEn" });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_NationalID",
                table: "Patients",
                column: "NationalID",
                unique: true,
                filter: "[NationalID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Nationality",
                table: "Patients",
                column: "Nationality");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PassportNumber",
                table: "Patients",
                column: "PassportNumber",
                unique: true,
                filter: "[PassportNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UpdatedBy",
                table: "Patients",
                column: "UpdatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppLookupDetail");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "AppLookupMaster");
        }
    }
}
