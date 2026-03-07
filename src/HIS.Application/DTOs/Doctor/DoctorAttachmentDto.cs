namespace HIS.Application.DTOs.Doctor;

public class DoctorAttachmentDto
{
    public Guid Oid { get; set; }
    public Guid DoctorId { get; set; }
    public Guid? AttachmentTypeId { get; set; }
    public string? AttachmentTypeName { get; set; }
    public string? FileName { get; set; }
    public string? FilePath { get; set; }
    public DateTime? UploadedAt { get; set; }
    public DateTime? CreatedAt { get; set; }
}
