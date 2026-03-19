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
    public sealed class GetDoctorScheduleListHandler : IRequestHandler<GetDoctorSchdeuleListQuery, IEnumerable<CreateSingleScheduleResponse>>
    {
        private readonly IMapper mapper;
        private readonly IDoctorScheduleMasterRepository repository;

        public GetDoctorScheduleListHandler(IMapper mapper,IDoctorScheduleMasterRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<IEnumerable<CreateSingleScheduleResponse>> Handle(GetDoctorSchdeuleListQuery request, CancellationToken cancellationToken)
        {
         
                var schedules = await repository.GetAllAsync(cancellationToken);
            
            return mapper.Map<IEnumerable<CreateSingleScheduleResponse>>(schedules);

        }
    }
}
