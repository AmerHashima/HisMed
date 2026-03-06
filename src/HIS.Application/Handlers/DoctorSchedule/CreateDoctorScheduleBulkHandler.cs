using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class CreateDoctorScheduleBulkHandler : IRequestHandler<CreateDoctorScheduleBulkCommand, List<DoctorScheduleBulkResponseDto>>
    {
        private readonly IMapper mapper;
        private readonly IDoctorScheduleRepository repository;

        public CreateDoctorScheduleBulkHandler(IMapper mapper,IDoctorScheduleRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<List<DoctorScheduleBulkResponseDto>> Handle(CreateDoctorScheduleBulkCommand request, CancellationToken cancellationToken)
        {
            var SchedulesList = mapper.Map<List<Domain.Entities.DoctorSchedule>>(request.DoctorSechduelList);
             var result = await repository.AddDoctorScheduelList(SchedulesList,cancellationToken);
            return mapper.Map<List<DoctorScheduleBulkResponseDto>>(result);
        }
    }
}
