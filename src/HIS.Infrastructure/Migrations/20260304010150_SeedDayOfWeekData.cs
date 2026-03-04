using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDayOfWeekData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppLookupMaster",
                columns: new[] { "Oid", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "IsSystem", "LookupCode", "LookupNameAr", "LookupNameEn", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000010"), new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Days Of The Week", false, false, "Days", "الايام", "Days", null, null });

            migrationBuilder.InsertData(
                table: "AppLookupDetail",
                columns: new[] { "Oid", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsActive", "IsDefault", "IsDeleted", "MasterID", "SortOrder", "UpdatedAt", "UpdatedBy", "ValueCode", "ValueNameAr", "ValueNameEn" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0010-000000000001"), new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, false, new Guid("10000000-0000-0000-0000-000000000010"), 1, null, null, "Sat", "السبت", "Saturday" },
                    { new Guid("10000000-0000-0000-0010-000000000002"), new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, false, new Guid("10000000-0000-0000-0000-000000000010"), 2, null, null, "Sun", "الاحد", "Sunday" },
                    { new Guid("10000000-0000-0000-0010-000000000003"), new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, false, new Guid("10000000-0000-0000-0000-000000000010"), 3, null, null, "Mon", "الاثنين", "Monday" },
                    { new Guid("10000000-0000-0000-0010-000000000004"), new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, false, new Guid("10000000-0000-0000-0000-000000000010"), 4, null, null, "Tue", "الثلاثاء", "Tuesday" },
                    { new Guid("10000000-0000-0000-0010-000000000005"), new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, false, new Guid("10000000-0000-0000-0000-000000000010"), 5, null, null, "Wed", "الاربعاء", "Wednesday" },
                    { new Guid("10000000-0000-0000-0010-000000000006"), new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, true, false, false, new Guid("10000000-0000-0000-0000-000000000010"), 6, null, null, "Thu", "الخميس", "Thursday" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppLookupDetail",
                keyColumn: "Oid",
                keyValue: new Guid("10000000-0000-0000-0010-000000000001"));

            migrationBuilder.DeleteData(
                table: "AppLookupDetail",
                keyColumn: "Oid",
                keyValue: new Guid("10000000-0000-0000-0010-000000000002"));

            migrationBuilder.DeleteData(
                table: "AppLookupDetail",
                keyColumn: "Oid",
                keyValue: new Guid("10000000-0000-0000-0010-000000000003"));

            migrationBuilder.DeleteData(
                table: "AppLookupDetail",
                keyColumn: "Oid",
                keyValue: new Guid("10000000-0000-0000-0010-000000000004"));

            migrationBuilder.DeleteData(
                table: "AppLookupDetail",
                keyColumn: "Oid",
                keyValue: new Guid("10000000-0000-0000-0010-000000000005"));

            migrationBuilder.DeleteData(
                table: "AppLookupDetail",
                keyColumn: "Oid",
                keyValue: new Guid("10000000-0000-0000-0010-000000000006"));

            migrationBuilder.DeleteData(
                table: "AppLookupMaster",
                keyColumn: "Oid",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"));
        }
    }
}
