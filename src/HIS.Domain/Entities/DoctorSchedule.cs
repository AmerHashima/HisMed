using HIS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.Domain.Entities;

[Table("DoctorSchedules")]
public class DoctorSchedule : BaseEntity
{
    [Required]
    public Guid DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    public virtual Doctor Doctor { get; set; } = null!;

    [Required]
    [Range(0, 6)] // 0=Sunday, 6=Saturday
    public int DayOfWeek { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    public TimeOnly EndTime { get; set; }

    public int SlotDurationMinutes { get; set; } = 15;

    public bool IsActive { get; set; } = true;
}