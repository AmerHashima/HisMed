using HIS.Application.DTOs.DoctorSchedule;
using HIS.Application.Queries.DoctorSchedule;
using MediatR;
using AutoMapper;
using HIS.Domain.Interfaces;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class GetDoctorScheduelByIdHandler : IRequestHandler<GetDoctorSchedualByIdQuery, CreateSingleScheduleResponse?>
    {
        private readonly IMapper mapper;
        private readonly IDoctorScheduleMasterRepository repository;

        public GetDoctorScheduelByIdHandler(IMapper mapper,IDoctorScheduleMasterRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async  Task<CreateSingleScheduleResponse?> Handle(GetDoctorSchedualByIdQuery request, CancellationToken cancellationToken)
        {
            var Schdeuel = await repository.GetByIdAsync(request.Id,cancellationToken);

            return Schdeuel == null ? null : mapper.Map<CreateSingleScheduleResponse>(Schdeuel);
        }
    }
}
