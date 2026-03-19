using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.DTOs.DoctorSchedule
{
    public class UpdateDoctorSchdeuelDto
    {
        public Guid Oid { get; set; }
        public Guid DoctorId { get; set; }
        public Guid DayOfWeekId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Guid StatusId { get; set; }
        public Guid BranchId { get; set; }
        public Guid SpecialtyId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsPriority { get; set; } = false;
        public float SlotDurationMinutes { get; set; } = 15;
    }
}
