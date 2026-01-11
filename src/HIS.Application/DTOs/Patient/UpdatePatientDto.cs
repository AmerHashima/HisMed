using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Patient;

public class UpdatePatientDto
{
    [Required]
    public Guid Oid { get; set; }

    /* ==== Identifiers ==== */
    [StringLength(20, ErrorMessage = "National ID cannot exceed 20 characters")]
    public string? NationalID { get; set; }

    [StringLength(20, ErrorMessage = "Passport number cannot exceed 20 characters")]
    public string? PassportNumber { get; set; }

    [Required(ErrorMessage = "Identifier type is required")]
    [StringLength(20, ErrorMessage = "Identifier type cannot exceed 20 characters")]
    public string IdentifierType { get; set; } = string.Empty;

    /* ==== Names (Arabic & English) ==== */
    [Required(ErrorMessage = "Arabic first name is required")]
    [StringLength(100, ErrorMessage = "Arabic first name cannot exceed 100 characters")]
    public string FirstNameAr { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "Arabic middle name cannot exceed 100 characters")]
    public string? MiddleNameAr { get; set; }

    [Required(ErrorMessage = "Arabic last name is required")]
    [StringLength(100, ErrorMessage = "Arabic last name cannot exceed 100 characters")]
    public string LastNameAr { get; set; } = string.Empty;

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
    public char Gender { get; set; }

    [Required(ErrorMessage = "Birth date is required")]
    public DateOnly BirthDate { get; set; }

    [StringLength(20, ErrorMessage = "Marital status cannot exceed 20 characters")]
    public string? MaritalStatus { get; set; }

    [StringLength(50, ErrorMessage = "Nationality cannot exceed 50 characters")]
    public string? Nationality { get; set; }

    [StringLength(5, ErrorMessage = "Blood group cannot exceed 5 characters")]
    public string? BloodGroup { get; set; }

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

    /* ==== Address ==== */
    [Required(ErrorMessage = "Address line 1 is required")]
    [StringLength(200, ErrorMessage = "Address line 1 cannot exceed 200 characters")]
    public string AddressLine1 { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "Address line 2 cannot exceed 200 characters")]
    public string? AddressLine2 { get; set; }

    [Required(ErrorMessage = "City is required")]
    [StringLength(100, ErrorMessage = "City cannot exceed 100 characters")]
    public string City { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "State cannot exceed 100 characters")]
    public string? State { get; set; }

    [StringLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
    public string? PostalCode { get; set; }

    [Required(ErrorMessage = "Country is required")]
    [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
    public string Country { get; set; } = string.Empty;

    /* ==== Emergency Contact ==== */
    [StringLength(150, ErrorMessage = "Emergency contact name cannot exceed 150 characters")]
    public string? EmergencyName { get; set; }

    [StringLength(50, ErrorMessage = "Emergency relation cannot exceed 50 characters")]
    public string? EmergencyRelation { get; set; }

    [Phone(ErrorMessage = "Invalid emergency mobile number format")]
    [StringLength(20, ErrorMessage = "Emergency mobile cannot exceed 20 characters")]
    public string? EmergencyMobile { get; set; }

    public bool IsActive { get; set; } = true;
}