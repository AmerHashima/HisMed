using HIS.Application.DTOs.DoctorScheduleException;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Commands.DoctorScheduelException
{
    public sealed record CreateDoctorScheduelExceptionCommand(CreateDoctorScheduleExceptionDto DoctorScheduelException) :IRequest<DoctorScheduleExceptionResponseDto>
    {
    }
}
