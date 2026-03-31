using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Application.Services;
using HIS.Domain.Common;
using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class DoctorScheduleHandler : IRequestHandler<CreateDoctorScheduleCommand, CreateSingleScheduleResponse>
    {
        private readonly IDoctorScheduleMasterRepository _masterRepo;
        private readonly IMapper _mapper;
        private readonly IDoctorScheduleValidationService service;

        public DoctorScheduleHandler(IDoctorScheduleMasterRepository masterRepo, IMapper mapper,IDoctorScheduleValidationService service)
        {
            _masterRepo = masterRepo;
            _mapper = mapper;
            this.service = service;
        }

        public async Task<CreateSingleScheduleResponse> Handle(CreateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            var master = _mapper.Map<DoctorScheduleMaster>(request.DoctorSechedule);
            if (await service.HasOverLap(master.BranchId, master.SpecialtyId, master.DoctorId, master.Details, cancellation: cancellationToken))
            {
               throw  new InvalidOperationException($"A doctor with Specialty: {master.SpecialtyId} and Branch: {master.BranchId} " +
                    "already has an overlapping time slot on the same day.");
            }

            var created = await _masterRepo.AddAsync(master);
            var result = await _masterRepo.GetByIdAsync(created.Oid, cancellationToken);

            return _mapper.Map<CreateSingleScheduleResponse>(result);
        }
    }
}
