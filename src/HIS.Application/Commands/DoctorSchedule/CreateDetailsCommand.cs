using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Commands.DoctorSchedule
{
    public sealed record CreateDetailsCommand(CreateDetailDto details):IRequest<DoctorSchedulesListDto>
    {
    }
}
