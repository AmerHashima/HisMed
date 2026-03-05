using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HIS.Domain.Common;

namespace HIS.Domain.Entities;

[Table("PatientAttachment")]
public class PatientAttachment : BaseEntity
{
    [Required]
    public Guid PatientId { get; set; }
    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    public Guid? AttachmentTypeId { get; set; }
    [ForeignKey(nameof(AttachmentTypeId))]
    public virtual AppLookupDetail? AttachmentType { get; set; }

    [MaxLength(500)]
    public string? FileName { get; set; }

    [MaxLength(1000)]
    public string? FilePath { get; set; }

    [MaxLength(20)]
    public string? FileExtension { get; set; }

    public long? FileSize { get; set; }

    public DateTime? UploadedAt { get; set; } = DateTime.UtcNow;
}
