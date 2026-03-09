using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.DoctorSchedule
{
    public class CreateDoctorScheduleDto
    {
        
        [Required(ErrorMessage ="DoctorID is Required")]
        public Guid DoctorId { get; set; }  

        [Required(ErrorMessage ="Day of week is Required")]
        
        public Guid DayOfWeekId { get; set; }

        [Required(ErrorMessage ="StartTime is Required")]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage ="EndDate is Required")]
        public TimeOnly EndTime { get; set; }
        [Required(ErrorMessage = "EndDate is Required")]
        public float SlotDurationMinutes { get; set; }

    }
}
