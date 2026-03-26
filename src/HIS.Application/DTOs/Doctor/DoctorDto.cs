namespace HIS.Application.DTOs.Doctor;

public class DoctorDto
{
    public Guid Oid { get; set; }
    public Guid? UserId { get; set; }
    public string? Username { get; set; }

    /* ==== Names ==== */
    public string? FirstNameAr { get; set; }
    public string? MiddleNameAr { get; set; }
    public string? LastNameAr { get; set; }
    public string? FirstNameEn { get; set; }
    public string? MiddleNameEn { get; set; }
    public string? LastNameEn { get; set; }

    /* ==== Demographics ==== */
    public Guid? GenderId { get; set; }
    public string? GenderName { get; set; }

    /* ==== License ==== */
    public string? LicenseNumber { get; set; }
    public Guid? LicenseTypeId { get; set; }
    public string? LicenseTypeName { get; set; }
    public DateOnly? LicenseIssueDate { get; set; }
    public DateOnly? LicenseExpiryDate { get; set; }

    /* ==== Specialty ==== */
    public Guid? SpecialtyId { get; set; }
    public string? SpecialtyNameEn { get; set; }
    public string? SpecialtyNameAr { get; set; }
    public Guid? SubSpecialtyId { get; set; }
    public string? SubSpecialtyName { get; set; }

    /* ==== Department ==== */
    public Guid? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }

    /* ==== Contact ==== */
    public string? Mobile { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    /* ==== Professional ==== */
    public int? YearsOfExperience { get; set; }
    public decimal? ConsultationFee { get; set; }

    /* ==== Branch ==== */
    public Guid? BranchId { get; set; }
    public string? BranchName { get; set; }

    /* ==== NPHIES ==== */
    public string? NphiesProviderId { get; set; }
    public string? NphiesLicenseNumber { get; set; }
    public bool IsNphiesEnabled { get; set; }

    public bool IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    /* ==== Full Names ==== */
    public string? FullNameAr { get; set; }
    public string? FullNameEn { get; set; }
}