using HIS.Application.DTOs.DoctorSchedule;
using HIS.Application.Queries.DoctorSchedule;
using MediatR;
using AutoMapper;
using HIS.Domain.Interfaces;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class GetDoctorScheduelByIdHandler : IRequestHandler<GetDoctorSchedualByIdQuery, DoctorScheduleDto?>
    {
        private readonly IMapper mapper;
        private readonly IDoctorScheduleRepository repository;

        public GetDoctorScheduelByIdHandler(IMapper mapper,IDoctorScheduleRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async  Task<DoctorScheduleDto?> Handle(GetDoctorSchedualByIdQuery request, CancellationToken cancellationToken)
        {
            var Schdeuel = await repository.GetScheduelByIdAsync(request.Id,cancellationToken);

            return Schdeuel == null ? null : mapper.Map<DoctorScheduleDto>(Schdeuel);
        }
    }
}
