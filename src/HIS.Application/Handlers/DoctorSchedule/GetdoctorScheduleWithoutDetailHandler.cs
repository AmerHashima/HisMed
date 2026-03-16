using AutoMapper;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Application.Queries.DoctorSchedule;
using HIS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class GetdoctorScheduleWithoutDetailHandler : IRequestHandler<GetDoctorScheduleWithoutDetailsQuery, IEnumerable<ScheduleWithNoDetailsDto>>
    {
        private readonly IDoctorScheduleMasterRepository repository;
        private readonly IMapper mapper;

        public GetdoctorScheduleWithoutDetailHandler(IDoctorScheduleMasterRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<ScheduleWithNoDetailsDto>> Handle(GetDoctorScheduleWithoutDetailsQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetDoctorSchedule();
            return mapper.Map<IEnumerable<ScheduleWithNoDetailsDto>>(result);
        }
    }
}
