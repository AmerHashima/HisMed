using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Patient;

public class UpdatePatientDto
{
    [Required]
    public Guid Oid { get; set; }

    /* ==== Identifiers ==== */
    [Required(ErrorMessage = "Identity type is required")]
    public Guid IdentityTypeLookupId { get; set; }

    [Required(ErrorMessage = "Identity number is required")]
    [StringLength(20, ErrorMessage = "Identity number cannot exceed 20 characters")]
    public string IdentityNumber { get; set; } = string.Empty;

    /* ==== Names (Arabic) ==== */
    [Required(ErrorMessage = "Arabic first name is required")]
    [StringLength(100)]
    public string FirstNameAr { get; set; } = string.Empty;

    [StringLength(100)]
    public string? MiddleNameAr { get; set; }

    [Required(ErrorMessage = "Arabic last name is required")]
    [StringLength(100)]
    public string LastNameAr { get; set; } = string.Empty;

    /* ==== Names (English) ==== */
    [Required(ErrorMessage = "English first name is required")]
    [StringLength(100)]
    public string FirstNameEn { get; set; } = string.Empty;

    [StringLength(100)]
    public string? MiddleNameEn { get; set; }

    [Required(ErrorMessage = "English last name is required")]
    [StringLength(100)]
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
    [Phone]
    [StringLength(20)]
    public string Mobile { get; set; } = string.Empty;

    [Phone]
    [StringLength(20)]
    public string? Phone { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }

    /* ==== Branch ==== */
    [Required(ErrorMessage = "Branch is required")]
    public Guid BranchId { get; set; }

    public bool IsActive { get; set; }
}