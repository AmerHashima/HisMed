using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.Domain.Entities
{
    public class DoctorScheduleDetail:BaseEntity
    {

        public Guid DayOfWeekId { get; set; }
        [ForeignKey(nameof(DayOfWeekId))]
        public virtual AppLookupDetail DayOfweek { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        public float SlotDurationMinutes { get; set; } = 15;
        public Guid MasterId { get; set; }
        [ForeignKey("MasterId")]
        public DoctorScheduleMaster master { get; set; }
    }
}
