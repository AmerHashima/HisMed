using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Doctor;

public class CreateDoctorBranchDto
{
    [Required(ErrorMessage = "Doctor ID is required")]
    public Guid DoctorId { get; set; }

    public Guid? BranchId { get; set; }

    public bool IsDefault { get; set; }
}
