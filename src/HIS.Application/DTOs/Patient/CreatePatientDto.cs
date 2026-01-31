using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Patient;

public class CreatePatientDto
{
    /* ==== Identifiers ==== */
    [Required(ErrorMessage = "Identity type is required")]
    public Guid IdentityTypeLookupId { get; set; }

    [Required(ErrorMessage = "Identity number is required")]
    [StringLength(20, ErrorMessage = "Identity number cannot exceed 20 characters")]
    public string IdentityNumber { get; set; } = string.Empty;

    /* ==== Names (Arabic) ==== */
    [Required(ErrorMessage = "Arabic first name is required")]
    [StringLength(100, ErrorMessage = "Arabic first name cannot exceed 100 characters")]
    public string FirstNameAr { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "Arabic middle name cannot exceed 100 characters")]
    public string? MiddleNameAr { get; set; }

    [Required(ErrorMessage = "Arabic last name is required")]
    [StringLength(100, ErrorMessage = "Arabic last name cannot exceed 100 characters")]
    public string LastNameAr { get; set; } = string.Empty;

    /* ==== Names (English) ==== */
    [Required(ErrorMessage = "English first name is required")]
    [StringLength(100, ErrorMessage = "English first name cannot exceed 100 characters")]
    public string FirstNameEn { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "English middle name cannot exceed 100 characters")]
    public string? MiddleNameEn { get; set; }

    [Required(ErrorMessage = "English last name is required")]
    [StringLength(100, ErrorMessage = "English last name cannot exceed 100 characters")]
    public string LastNameEn { get; set; } = string.Empty;

    /* ==== Demographics ==== */
    [Required(ErrorMessage = "Gender is required")]
    public Guid GenderLookupId { get; set; }

    [Required(ErrorMessage = "Birth date is required")]
    public DateOnly BirthDate { get; set; }

    public Guid? NationalityLookupId { get; set; }
    public Guid? MaritalStatusLookupId { get; set; }
    public Guid? BloodGroupLookupId { get; set; }

    /* ==== Contact ==== */
    [Required(ErrorMessage = "Mobile number is required")]
    [Phone(ErrorMessage = "Invalid mobile number format")]
    [StringLength(20, ErrorMessage = "Mobile number cannot exceed 20 characters")]
    public string Mobile { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Invalid phone number format")]
    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
    public string? Phone { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    public string? Email { get; set; }

    /* ==== Branch ==== */
    [Required(ErrorMessage = "Branch is required")]
    public Guid BranchId { get; set; }
}