using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.HospitalBranch;

public class CreateHospitalBranchDto
{
    [Required(ErrorMessage = "Branch code is required")]
    [StringLength(20, ErrorMessage = "Branch code cannot exceed 20 characters")]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "Branch name is required")]
    [StringLength(100, ErrorMessage = "Branch name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
    public string? Address { get; set; }

    [StringLength(100, ErrorMessage = "City cannot exceed 100 characters")]
    public string? City { get; set; }

    [StringLength(100, ErrorMessage = "State cannot exceed 100 characters")]
    public string? State { get; set; }

    [StringLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
    public string? PostalCode { get; set; }

    [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
    public string? Country { get; set; }

    public bool IsActive { get; set; } = true;
}