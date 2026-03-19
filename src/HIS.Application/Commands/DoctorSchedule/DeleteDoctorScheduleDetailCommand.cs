using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Commands.DoctorSchedule
{
    public sealed record DeleteDoctorScheduleDetailCommand(Guid Id):IRequest<bool>
    {
    }
}
