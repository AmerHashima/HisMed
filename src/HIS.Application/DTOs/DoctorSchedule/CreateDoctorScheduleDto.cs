using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.DoctorSchedule
{
    public class CreateDoctorScheduleDto
    {

        public Guid DoctorId { get; set; }
        public Guid StatusId { get; set; }
        public Guid BranchId { get; set; }     
        public Guid SpecialtyId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsPriority { get; set; } = false;
        [Required]
        public Guid DayOfWeekId { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        public float SlotDurationMinutes { get; set; } = 15;


    }
}
