using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Application.Services;
using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using MediatR;
using System.Diagnostics.Metrics;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class CreateDoctorScheduleBulkHandler : IRequestHandler<CreateDoctorScheduleBulkCommand, DoctorScheduleMasterDetailDto>
    {
        private readonly IMapper mapper;
        private readonly IDoctorScheduleMasterRepository repository;
        private readonly IDoctorScheduleValidationService service;

        public CreateDoctorScheduleBulkHandler(IMapper mapper,IDoctorScheduleMasterRepository repository,IDoctorScheduleValidationService service)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.service = service;
        }
        public async Task<DoctorScheduleMasterDetailDto> Handle(CreateDoctorScheduleBulkCommand request, CancellationToken cancellationToken)
        {
            var scheduleMaster = mapper.Map<DoctorScheduleMaster>(request.DoctorSechduel);
            if (await service.HasOverLap(scheduleMaster.BranchId, scheduleMaster.SpecialtyId, scheduleMaster.DoctorId, scheduleMaster.Details, cancellation: cancellationToken))
            {
               throw new InvalidOperationException($"A doctor with Speciality: {scheduleMaster.SpecialtyId} and Branch: {scheduleMaster.BranchId} " +
                 "already has an overlapping time slot on the same day.");
            }

            var schedule = await repository.AddAsync(scheduleMaster, cancellationToken);
            var result = await repository.GetByIdAsync(schedule.Oid, cancellationToken);

            return mapper.Map<DoctorScheduleMasterDetailDto>(result);
        }
    }
}
