using HIS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.Domain.Entities;

[Table("DoctorScheduleExceptions")]
public class DoctorScheduleException : BaseEntity
{
    [Required]
    public Guid DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    public virtual Doctor Doctor { get; set; } = null!;

    [Required]
    public DateOnly ExceptionDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    [MaxLength(30)]
    public string? ExceptionType { get; set; } // Holiday, Leave, Conference

    [MaxLength(255)]
    public string? Reason { get; set; }
}