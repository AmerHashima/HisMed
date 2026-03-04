using HIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.DTOs.DoctorScheduleException
{
    public class CreateDoctorScheduleExceptionDto
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        
        [Required]
        public DateOnly ExceptionDate { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }
        public Guid DayOfWeekId { get; set; }
       
        [MaxLength(30)]
        public string? ExceptionType { get; set; } // Holiday, Leave, Conference

        [MaxLength(255)]
        public string? Reason { get; set; }
    }
    

}
