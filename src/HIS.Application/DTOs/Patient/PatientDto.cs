namespace HIS.Application.DTOs.Patient;

public class PatientDto
{
    public Guid Oid { get; set; }
    
    /* ==== Identifiers ==== */
    public string MRN { get; set; } = string.Empty;
    
    // Foreign Keys
    public Guid IdentityTypeLookupId { get; set; }
    public string IdentityNumber { get; set; } = string.Empty;
    
    // Lookup Display Values
    public string? IdentityTypeName { get; set; }

    /* ==== Names (Arabic & English) ==== */
    public string FirstNameAr { get; set; } = string.Empty;
    public string? MiddleNameAr { get; set; }
    public string LastNameAr { get; set; } = string.Empty;
    public string FullNameAr { get; set; } = string.Empty;
    
    public string FirstNameEn { get; set; } = string.Empty;
    public string? MiddleNameEn { get; set; }
    public string LastNameEn { get; set; } = string.Empty;
    public string FullNameEn { get; set; } = string.Empty;

    /* ==== Demographics ==== */
    public Guid GenderLookupId { get; set; }
    public string? GenderName { get; set; }
    public DateOnly BirthDate { get; set; }
    public int Age { get; set; }
    
    public Guid? NationalityLookupId { get; set; }
    public string? NationalityName { get; set; }
    
    public Guid? MaritalStatusLookupId { get; set; }
    public string? MaritalStatusName { get; set; }
    
    public Guid? BloodGroupLookupId { get; set; }
    public string? BloodGroupName { get; set; }

    /* ==== Contact ==== */
    public string Mobile { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }

    /* ==== Branch ==== */
    public Guid BranchId { get; set; }
    public string? BranchName { get; set; }

    /* ==== System Fields ==== */
    public bool IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}