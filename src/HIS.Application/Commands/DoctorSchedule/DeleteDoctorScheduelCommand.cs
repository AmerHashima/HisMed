using HIS.Application.DTOs.DoctorSchedule;
using MediatR;

namespace HIS.Application.Commands.DoctorSchedule
{
    public sealed record DeleteDoctorScheduelCommand(Guid Id):IRequest<bool>
    {

    }
}
