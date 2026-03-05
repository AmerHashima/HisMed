using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Patient;

public class CreatePatientContactDto
{
    [Required(ErrorMessage = "Patient ID is required")]
    public Guid PatientId { get; set; }

    [StringLength(200, ErrorMessage = "Contact name cannot exceed 200 characters")]
    public string? ContactName { get; set; }

    public Guid? RelationshipId { get; set; }

    [StringLength(20, ErrorMessage = "Mobile cannot exceed 20 characters")]
    public string? Mobile { get; set; }

    [StringLength(20, ErrorMessage = "Phone cannot exceed 20 characters")]
    public string? Phone { get; set; }

    [StringLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }
}
