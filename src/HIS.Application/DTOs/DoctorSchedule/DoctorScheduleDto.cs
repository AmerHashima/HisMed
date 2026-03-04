using HIS.Application.DTOs.Doctor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.DTOs.DoctorSchedule
{
    public class DoctorScheduleDto
    {
        public Guid DoctorId { get; set; }

        public string DayOfWeekNameEn { get; set; }

      public string DayOfWeekNameAr { get; set; }
        public TimeOnly StartTime { get; set; }

      
        public TimeOnly EndTime { get; set; }

        public float SlotDurationMinutes { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
