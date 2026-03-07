using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Doctor;

public class CreateDoctorDto
{
    public Guid? UserId { get; set; }

    /* ==== Names (Arabic) ==== */
    [StringLength(200, ErrorMessage = "Arabic first name cannot exceed 200 characters")]
    public string? FirstNameAr { get; set; }

    [StringLength(200, ErrorMessage = "Arabic middle name cannot exceed 200 characters")]
    public string? MiddleNameAr { get; set; }

    [StringLength(200, ErrorMessage = "Arabic last name cannot exceed 200 characters")]
    public string? LastNameAr { get; set; }

    /* ==== Names (English) ==== */
    [StringLength(200, ErrorMessage = "English first name cannot exceed 200 characters")]
    public string? FirstNameEn { get; set; }

    [StringLength(200, ErrorMessage = "English middle name cannot exceed 200 characters")]
    public string? MiddleNameEn { get; set; }

    [StringLength(200, ErrorMessage = "English last name cannot exceed 200 characters")]
    public string? LastNameEn { get; set; }

    /* ==== Demographics ==== */
    public Guid? GenderId { get; set; }

    /* ==== License ==== */
    [StringLength(100, ErrorMessage = "License number cannot exceed 100 characters")]
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
    [StringLength(20, ErrorMessage = "Mobile cannot exceed 20 characters")]
    public string? Mobile { get; set; }

    [StringLength(20, ErrorMessage = "Phone cannot exceed 20 characters")]
    public string? Phone { get; set; }

    [StringLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }

    /* ==== Professional ==== */
    public int? YearsOfExperience { get; set; }
    public decimal? ConsultationFee { get; set; }

    /* ==== Branch ==== */
    public Guid? BranchId { get; set; }

    /* ==== NPHIES ==== */
    [StringLength(100, ErrorMessage = "NPHIES Provider ID cannot exceed 100 characters")]
    public string? NphiesProviderId { get; set; }

    [StringLength(100, ErrorMessage = "NPHIES License Number cannot exceed 100 characters")]
    public string? NphiesLicenseNumber { get; set; }

    public bool IsNphiesEnabled { get; set; } = false;
    public bool IsActive { get; set; } = true;
}