using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.DTOs.DoctorSchedule
{
    public class CreateDetailsDto
    {
        public Guid MasterId { get; set; }
        public Guid DayOfWeekId { get; set; }

        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }

        public float SlotDurationMinutes { get; set; }
    }
}
