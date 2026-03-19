using HIS.Application.DTOs.DoctorSchedule;
using MediatR;

namespace HIS.Application.Queries.DoctorSchedule
{
    public sealed record GetDoctorSchedualByIdQuery(Guid Id):IRequest<CreateSingleScheduleResponse?>
    {
    }
}
