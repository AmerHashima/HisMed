using AutoMapper;
using HIS.Application.DTOs.Doctor;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Application.Queries.DoctorSchedule;
using HIS.Domain.Common;
using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class GetDoctorScheduleListHandler : IRequestHandler<GetDoctorSchdeuleListQuery, IEnumerable<DoctorScheduleDto>>
    {
        private readonly IMapper mapper;
        private readonly IDoctorScheduleRepository repository;

        public GetDoctorScheduleListHandler(IMapper mapper,IDoctorScheduleRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<IEnumerable<DoctorScheduleDto>> Handle(GetDoctorSchdeuleListQuery request, CancellationToken cancellationToken)
        {
            if (request.DoctorId.HasValue)
            {
                var result = await repository.GetSchdeuleByDoctorIdAsync(request.DoctorId.Value, cancellationToken);
                return mapper.Map<IEnumerable<DoctorScheduleDto>>(result);
            }
           else if (request.StartTime.HasValue)
            {
                var result = await repository.GetSchdeulesByStartTime(request.StartTime,cancellationToken);
            }
            
                var schedules = await repository.GetAllAsync(cancellationToken);
            

            return mapper.Map<IEnumerable<DoctorScheduleDto>>(schedules);

        }
    }
}
