using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.DoctorScheduleException;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Queries.DoctorSceduelException
{
    public sealed record class GetAllDoctorsScheduleExceptionQuery(QueryRequest QueryRequest) :IRequest<PagedResult<DoctorScheduleExceptionResponseDto>>
    {
    }
}
