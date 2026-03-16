using HIS.Application.DTOs.DoctorSchedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Queries.DoctorSchedule
{
    public sealed class GetDoctorScheduleWithoutDetailsQuery:IRequest<IEnumerable<ScheduleWithNoDetailsDto>>
    {
    }
}
