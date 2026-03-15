using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.DoctorSchedule;
using MediatR;

namespace HIS.Application.Queries.DoctorSchedule
{
    public sealed record GetDoctorSchdeuleListQuery(Guid? DoctorId, TimeOnly? StartTime):IRequest<IEnumerable<DoctorScheduleDto>>
    {
    }
}
