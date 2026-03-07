using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HIS.Domain.Common;

namespace HIS.Domain.Entities;

[Table("DoctorAttachment")]
public class DoctorAttachment : BaseEntity
{
    [Required]
    public Guid DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    public virtual Doctor Doctor { get; set; } = null!;

    public Guid? AttachmentTypeId { get; set; }
    [ForeignKey(nameof(AttachmentTypeId))]
    public virtual AppLookupDetail? AttachmentType { get; set; }

    [MaxLength(500)]
    public string? FileName { get; set; }

    [MaxLength(1000)]
    public string? FilePath { get; set; }

    public DateTime? UploadedAt { get; set; } = DateTime.UtcNow;
}
