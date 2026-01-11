using HIS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.Domain.Entities;

[Table("Patients")]
public class Patient : BaseEntity
{
    /* ==== Identifiers ==== */
    [Required]
    [MaxLength(50)]
    public string MRN { get; set; } = string.Empty; // Medical Record Number

    [MaxLength(20)]
    public string? NationalID { get; set; }

    [MaxLength(20)]
    public string? PassportNumber { get; set; }

    [Required]
    [MaxLength(20)]
    public string IdentifierType { get; set; } = string.Empty; // NationalID / Passport / Iqama

    /* ==== Names (Arabic & English) ==== */
    [Required]
    [MaxLength(100)]
    public string FirstNameAr { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? MiddleNameAr { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastNameAr { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string FirstNameEn { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? MiddleNameEn { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastNameEn { get; set; } = string.Empty;

    // Computed properties - will be configured in EF configuration
    public string FullNameAr { get; set; } = string.Empty;
    public string FullNameEn { get; set; } = string.Empty;

    /* ==== Demographics ==== */
    [Required]
    public char Gender { get; set; } // M / F

    [Required]
    public DateOnly BirthDate { get; set; }

    [MaxLength(20)]
    public string? MaritalStatus { get; set; }

    [MaxLength(50)]
    public string? Nationality { get; set; }

    [MaxLength(5)]
    public string? BloodGroup { get; set; }

    /* ==== Contact ==== */
    [Required]
    [MaxLength(20)]
    public string Mobile { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    /* ==== Address ==== */
    [Required]
    [MaxLength(200)]
    public string AddressLine1 { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? AddressLine2 { get; set; }

    [Required]
    [MaxLength(100)]
    public string City { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? State { get; set; }

    [MaxLength(20)]
    public string? PostalCode { get; set; }

    [Required]
    [MaxLength(100)]
    public string Country { get; set; } = string.Empty;

    /* ==== Emergency Contact ==== */
    [MaxLength(150)]
    public string? EmergencyName { get; set; }

    [MaxLength(50)]
    public string? EmergencyRelation { get; set; }

    [MaxLength(20)]
    public string? EmergencyMobile { get; set; }

    /* ==== System Fields ==== */
    public bool IsActive { get; set; } = true;

    // Age calculation property
    public int Age => DateTime.Today.Year - BirthDate.Year - (DateTime.Today.DayOfYear < BirthDate.DayOfYear ? 1 : 0);

    // Navigation Properties
  //  public virtual ICollection<Appointment>? Appointments { get; set; }
   // public virtual ICollection<MedicalRecord>? MedicalRecords { get; set; }
}