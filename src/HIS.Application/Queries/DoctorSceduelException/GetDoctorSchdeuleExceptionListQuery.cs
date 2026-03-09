using HIS.Application.DTOs.DoctorScheduleException;
using MediatR;

namespace HIS.Application.Queries.DoctorSceduelException
{
    public  sealed record GetDoctorSchdeuleExceptionListQuery(Guid? DoctorId,DateOnly? ExceptionDate,TimeOnly? StartTime ):IRequest<IEnumerable<DoctorScheduleExceptionResponseDto>>
    {
    }
}
