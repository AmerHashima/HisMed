using HIS.Application.DTOs.DoctorSchedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Commands.DoctorSchedule
{
    public sealed record UpdateDoctorScheduleDetailsCommand(UpdateDetailsDto details):IRequest<DoctorSchedulesListDto>
    {

    }
}
