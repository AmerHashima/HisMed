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
        public List<DoctorSchedulesListDto> DoctorSchedules { get; set; }
    }
}
