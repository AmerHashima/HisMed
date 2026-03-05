using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Patient;

public class CreatePatientInsuranceDto
{
    [Required(ErrorMessage = "Patient ID is required")]
    public Guid PatientId { get; set; }

    public Guid? InsuranceCompanyId { get; set; }

    [StringLength(100, ErrorMessage = "Policy number cannot exceed 100 characters")]
    public string? PolicyNumber { get; set; }

    [StringLength(100, ErrorMessage = "Member ID cannot exceed 100 characters")]
    public string? MemberId { get; set; }

    [StringLength(50, ErrorMessage = "Insurance class cannot exceed 50 characters")]
    public string? InsuranceClass { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }
}
