using HIS.Application.DTOs.DoctorSchedule;
using MediatR;

namespace HIS.Application.Commands.DoctorSchedule
{
    public sealed record CreateDoctorScheduleCommand(CreateDoctorScheduleDto DoctorSechedule):IRequest<DoctorScheduleDto>
    {
    }
}
