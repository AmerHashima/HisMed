using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Patient;

public class CreatePatientAttachmentDto
{
    [Required(ErrorMessage = "Patient ID is required")]
    public Guid PatientId { get; set; }

    public Guid? AttachmentTypeId { get; set; }

    [StringLength(500, ErrorMessage = "File name cannot exceed 500 characters")]
    public string? FileName { get; set; }

    [StringLength(1000, ErrorMessage = "File path cannot exceed 1000 characters")]
    public string? FilePath { get; set; }

    [StringLength(20, ErrorMessage = "File extension cannot exceed 20 characters")]
    public string? FileExtension { get; set; }

    public long? FileSize { get; set; }
}
