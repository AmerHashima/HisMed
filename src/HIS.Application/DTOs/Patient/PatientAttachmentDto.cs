namespace HIS.Application.DTOs.Patient;

public class PatientAttachmentDto
{
    public Guid Oid { get; set; }
    public Guid PatientId { get; set; }
    public Guid? AttachmentTypeId { get; set; }
    public string? AttachmentTypeName { get; set; }
    public string? FileName { get; set; }
    public string? FilePath { get; set; }
    public string? FileExtension { get; set; }
    public long? FileSize { get; set; }
    public DateTime? UploadedAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
