using HIS.Domain.Entities;
using HIS.Infrastructure.Persistence;

namespace HIS.Infrastructure.Data;

public static class LookupSeeder
{
    public static async Task SeedLookupDataAsync(HISDbContext context)
    {
        // Gender Lookup
        if (!context.AppLookupMasters.Any(x => x.LookupCode == "GENDER"))
        {
            var genderMaster = new AppLookupMaster
            {
                LookupCode = "GENDER",
                LookupNameAr = "الجنس",
                LookupNameEn = "Gender",
                Description = "Gender options",
                IsSystem = true
            };

            context.AppLookupMasters.Add(genderMaster);
            await context.SaveChangesAsync();

            var genderDetails = new List<AppLookupDetail>
            {
                new() { LookupMasterID = genderMaster.Oid, ValueCode = "M", ValueNameAr = "ذكر", ValueNameEn = "Male", SortOrder = 1 },
                new() { LookupMasterID = genderMaster.Oid, ValueCode = "F", ValueNameAr = "أنثى", ValueNameEn = "Female", SortOrder = 2 }
            };

            context.AppLookupDetails.AddRange(genderDetails);
        }

        // Marital Status Lookup
        if (!context.AppLookupMasters.Any(x => x.LookupCode == "MARITAL_STATUS"))
        {
            var maritalMaster = new AppLookupMaster
            {
                LookupCode = "MARITAL_STATUS",
                LookupNameAr = "الحالة الاجتماعية",
                LookupNameEn = "Marital Status",
                Description = "Marital status options",
                IsSystem = true
            };

            context.AppLookupMasters.Add(maritalMaster);
            await context.SaveChangesAsync();

            var maritalDetails = new List<AppLookupDetail>
            {
                new() { LookupMasterID = maritalMaster.Oid, ValueCode = "SINGLE", ValueNameAr = "أعزب", ValueNameEn = "Single", SortOrder = 1, IsDefault = true },
                new() { LookupMasterID = maritalMaster.Oid, ValueCode = "MARRIED", ValueNameAr = "متزوج", ValueNameEn = "Married", SortOrder = 2 },
                new() { LookupMasterID = maritalMaster.Oid, ValueCode = "DIVORCED", ValueNameAr = "مطلق", ValueNameEn = "Divorced", SortOrder = 3 },
                new() { LookupMasterID = maritalMaster.Oid, ValueCode = "WIDOWED", ValueNameAr = "أرمل", ValueNameEn = "Widowed", SortOrder = 4 }
            };

            context.AppLookupDetails.AddRange(maritalDetails);
        }

        await context.SaveChangesAsync();
    }
}