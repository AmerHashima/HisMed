using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Doctor;

public class CreateDoctorAttachmentDto
{
    [Required(ErrorMessage = "Doctor ID is required")]
    public Guid DoctorId { get; set; }

    public Guid? AttachmentTypeId { get; set; }

    [StringLength(500, ErrorMessage = "File name cannot exceed 500 characters")]
    public string? FileName { get; set; }

    [StringLength(1000, ErrorMessage = "File path cannot exceed 1000 characters")]
    public string? FilePath { get; set; }
}
