using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.DTOs.DoctorScheduleException
{
   public class DoctorScheduleExceptionResponseDto
   {
        public Guid Oid { get; set; }  

        public Guid DoctorId { get; set; }

        public DateOnly ExceptionDate { get; set; }
        public string DayOfWeekNameAr { get; set; } =string.Empty;
        public string DayOfWeekNameEn { get; set; } = string.Empty;

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public string? ExceptionType { get; set; }

        public string? Reason { get; set; }
    }
}
