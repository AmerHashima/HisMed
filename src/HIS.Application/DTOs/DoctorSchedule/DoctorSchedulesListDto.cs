using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.DTOs.DoctorSchedule
{
    public class DoctorSchedulesListDto
    {
        public Guid DoctorId { get; set; }
        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public float SlotDurationMinutes { get; set; }
        public Guid DayOfWeekId { get; set; }
    }
}
