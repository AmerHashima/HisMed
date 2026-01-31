using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Doctor;

public class CreateDoctorDto
{
    [Required(ErrorMessage = "User ID is required")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "License number is required")]
    [StringLength(50, ErrorMessage = "License number cannot exceed 50 characters")]
    public string LicenseNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Specialty is required")]
    public Guid SpecialtyId { get; set; }

    [Required(ErrorMessage = "Department is required")]
    public Guid DepartmentLookupId { get; set; }

    [Required(ErrorMessage = "Branch is required")]
    public Guid BranchId { get; set; }

    [StringLength(50, ErrorMessage = "NPHIES Provider ID cannot exceed 50 characters")]
    public string? NphiesProviderId { get; set; }

    public bool IsNphiesEnabled { get; set; } = true;
    public bool IsActive { get; set; } = true;
}