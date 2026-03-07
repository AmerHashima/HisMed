using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Doctor;

public class UpdateDoctorDto
{
    [Required]
    public Guid Oid { get; set; }

    public Guid? UserId { get; set; }

    /* ==== Names (Arabic) ==== */
    [StringLength(200)]
    public string? FirstNameAr { get; set; }

    [StringLength(200)]
    public string? MiddleNameAr { get; set; }

    [StringLength(200)]
    public string? LastNameAr { get; set; }

    /* ==== Names (English) ==== */
    [StringLength(200)]
    public string? FirstNameEn { get; set; }

    [StringLength(200)]
    public string? MiddleNameEn { get; set; }

    [StringLength(200)]
    public string? LastNameEn { get; set; }

    /* ==== Demographics ==== */
    public Guid? GenderId { get; set; }

    /* ==== License ==== */
    [StringLength(100)]
    public string? LicenseNumber { get; set; }

    public Guid? LicenseTypeId { get; set; }
    public DateOnly? LicenseIssueDate { get; set; }
    public DateOnly? LicenseExpiryDate { get; set; }

    /* ==== Specialty ==== */
    public Guid? SpecialtyId { get; set; }
    public Guid? SubSpecialtyId { get; set; }

    /* ==== Department ==== */
    public Guid? DepartmentId { get; set; }

    /* ==== Contact ==== */
    [StringLength(20)]
    public string? Mobile { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(200)]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }

    /* ==== Professional ==== */
    public int? YearsOfExperience { get; set; }
    public decimal? ConsultationFee { get; set; }

    /* ==== Branch ==== */
    public Guid? BranchId { get; set; }

    /* ==== NPHIES ==== */
    [StringLength(100)]
    public string? NphiesProviderId { get; set; }

    [StringLength(100)]
    public string? NphiesLicenseNumber { get; set; }

    public bool IsNphiesEnabled { get; set; }
    public bool IsActive { get; set; }
}