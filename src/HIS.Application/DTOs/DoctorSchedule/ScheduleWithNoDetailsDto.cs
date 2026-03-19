using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.DTOs.DoctorSchedule
{
    public class ScheduleWithNoDetailsDto
    {
        public Guid Oid { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; }
        public Guid StatusId { get; set; }
        public string Status { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public Guid SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }   
        public bool IsActive { get; set; } = true;
        public bool IsPriority { get; set; } = false;
    }
}
