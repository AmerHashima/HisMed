using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.DTOs.DoctorSchedule
{
     public  class CreateDoctorScheduleBulkDto
     {
        
        public List<CreateDoctorScheduleDto>  DoctorSchedules{ get; set; }
     }
}
