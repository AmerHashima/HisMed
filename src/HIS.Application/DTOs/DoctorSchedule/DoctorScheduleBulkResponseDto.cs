using HIS.Application.DTOs.DoctorSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Commands.DoctorSchedule
{
    public class DoctorScheduleBulkResponseDto
    {
        public Guid DoctorId { get; set; }
        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public float SlotDurationMinutes { get; set; }

    }
}
