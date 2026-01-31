using HIS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.Domain.Entities;

[Table("DoctorTimeSlots")]
public class DoctorTimeSlot : BaseEntity
{
    [Required]
    public Guid DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    public virtual Doctor Doctor { get; set; } = null!;

    [Required]
    public DateOnly SlotDate { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    public TimeOnly EndTime { get; set; }

    public bool IsBooked { get; set; } = false;

    public Guid? AppointmentId { get; set; }
    [ForeignKey(nameof(AppointmentId))]
    public virtual Appointment? Appointment { get; set; }
}