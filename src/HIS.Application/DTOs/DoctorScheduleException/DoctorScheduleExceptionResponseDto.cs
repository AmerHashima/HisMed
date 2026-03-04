using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.DTOs.DoctorScheduleException
{
   public class DoctorScheduleExceptionResponseDto
   {
        public Guid Id { get; set; }  

        public Guid DoctorId { get; set; }

        public DateOnly ExceptionDate { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public string? ExceptionType { get; set; }

        public string? Reason { get; set; }
    }
}
