using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addPatientDatamain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_BloodGroup",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Nationality",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "BloodGroup",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "IdentifierType",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "Patients",
                newName: "IdentifierValue");

            migrationBuilder.AddColumn<Guid>(
                name: "BloodGroupId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdentifierTypeId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MaritalStatusId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NationalityId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_BloodGroupId",
                table: "Patients",
                column: "BloodGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CountryId",
                table: "Patients",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_IdentifierTypeId",
                table: "Patients",
                column: "IdentifierTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MaritalStatusId",
                table: "Patients",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_NationalityId",
                table: "Patients",
                column: "NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AppLookupDetail_BloodGroupId",
                table: "Patients",
                column: "BloodGroupId",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AppLookupDetail_CountryId",
                table: "Patients",
                column: "CountryId",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AppLookupDetail_IdentifierTypeId",
                table: "Patients",
                column: "IdentifierTypeId",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AppLookupDetail_MaritalStatusId",
                table: "Patients",
                column: "MaritalStatusId",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AppLookupDetail_NationalityId",
                table: "Patients",
                column: "NationalityId",
                principalTable: "AppLookupDetail",
                principalColumn: "Oid",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AppLookupDetail_BloodGroupId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AppLookupDetail_CountryId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AppLookupDetail_IdentifierTypeId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AppLookupDetail_MaritalStatusId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AppLookupDetail_NationalityId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_BloodGroupId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_CountryId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_IdentifierTypeId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_MaritalStatusId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_NationalityId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "BloodGroupId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "IdentifierTypeId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MaritalStatusId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "IdentifierValue",
                table: "Patients",
                newName: "Nationality");

            migrationBuilder.AddColumn<string>(
                name: "BloodGroup",
                table: "Patients",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdentifierType",
                table: "Patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                table: "Patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_BloodGroup",
                table: "Patients",
                column: "BloodGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Nationality",
                table: "Patients",
                column: "Nationality");
        }
    }
}
