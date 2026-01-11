namespace HIS.Application.DTOs.Patient;

public class PatientDto
{
    public Guid Oid { get; set; }
    
    /* ==== Identifiers ==== */
    public string MRN { get; set; } = string.Empty;
    public string? NationalID { get; set; }
    public string? PassportNumber { get; set; }
    public string IdentifierType { get; set; } = string.Empty;

    /* ==== Names (Arabic & English) ==== */
    public string FirstNameAr { get; set; } = string.Empty;
    public string? MiddleNameAr { get; set; }
    public string LastNameAr { get; set; } = string.Empty;
    public string FirstNameEn { get; set; } = string.Empty;
    public string? MiddleNameEn { get; set; }
    public string LastNameEn { get; set; } = string.Empty;
    public string FullNameAr { get; set; } = string.Empty;
    public string FullNameEn { get; set; } = string.Empty;

    /* ==== Demographics ==== */
    public char Gender { get; set; }
    public DateOnly BirthDate { get; set; }
    public int Age { get; set; }
    public string? MaritalStatus { get; set; }
    public string? Nationality { get; set; }
    public string? BloodGroup { get; set; }

    /* ==== Contact ==== */
    public string Mobile { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }

    /* ==== Address ==== */
    public string AddressLine1 { get; set; } = string.Empty;
    public string? AddressLine2 { get; set; }
    public string City { get; set; } = string.Empty;
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string Country { get; set; } = string.Empty;

    /* ==== Emergency Contact ==== */
    public string? EmergencyName { get; set; }
    public string? EmergencyRelation { get; set; }
    public string? EmergencyMobile { get; set; }

    /* ==== System Fields ==== */
    public bool IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}