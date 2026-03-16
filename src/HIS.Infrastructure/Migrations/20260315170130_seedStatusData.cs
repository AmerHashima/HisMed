using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedStatusData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppLookupMaster",
                columns: new[] { "Oid", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "IsSystem", "LookupCode", "LookupNameAr", "LookupNameEn", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000011"), new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Doctor Status", false, false, "Status", "الحاله", "Status", null, null });

            migrationBuilder.InsertData(
                table: "AppLookupDetail",
                columns: new[] { "Oid", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsActive", "IsDefault", "IsDeleted", "MasterID", "SortOrder", "UpdatedAt", "UpdatedBy", "ValueCode", "ValueNameAr", "ValueNameEn" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0011-000000000001"), new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, false, new Guid("10000000-0000-0000-0000-000000000011"), 1, null, null, "Avl", "متاح", "Available" },
                    { new Guid("10000000-0000-0000-0011-000000000002"), new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, false, new Guid("10000000-0000-0000-0000-000000000011"), 2, null, null, "NotAvl", "غير متاح", "Not Available" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppLookupDetail",
                keyColumn: "Oid",
                keyValue: new Guid("10000000-0000-0000-0011-000000000001"));

            migrationBuilder.DeleteData(
                table: "AppLookupDetail",
                keyColumn: "Oid",
                keyValue: new Guid("10000000-0000-0000-0011-000000000002"));

            migrationBuilder.DeleteData(
                table: "AppLookupMaster",
                keyColumn: "Oid",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"));
        }
    }
}
