using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class CreateDoctorScheduleBulkHandler : IRequestHandler<CreateDoctorScheduleBulkCommand, DoctorScheduleMasterDetailDto>
    {
        private readonly IMapper mapper;
        private readonly IDoctorScheduleMasterRepository repository;

        public CreateDoctorScheduleBulkHandler(IMapper mapper,IDoctorScheduleMasterRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<DoctorScheduleMasterDetailDto> Handle(CreateDoctorScheduleBulkCommand request, CancellationToken cancellationToken)
        {
            var scheduleMaster = mapper.Map<DoctorScheduleMaster>(request.DoctorSechduel);

            var schedule = await repository.AddAsync(scheduleMaster, cancellationToken);
            var result = await repository.GetByIdAsync(schedule.Oid, cancellationToken);

            return mapper.Map<DoctorScheduleMasterDetailDto>(result);
        }
    }
}
