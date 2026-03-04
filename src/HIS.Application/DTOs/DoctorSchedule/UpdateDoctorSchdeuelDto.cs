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
        [Required(ErrorMessage = "DoctorID is Required")]
        public Guid DoctorId { get; set; }

        [Required(ErrorMessage = "Day of week is Required")]

        public Guid DayOfWeekId { get; set; }

        [Required(ErrorMessage = "StartTime is Required")]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "EndDate is Required")]
        public TimeOnly EndTime { get; set; }
    }
}
