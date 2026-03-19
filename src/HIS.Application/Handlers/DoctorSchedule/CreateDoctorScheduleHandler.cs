using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
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

        public DoctorScheduleHandler(IDoctorScheduleMasterRepository masterRepo, IMapper mapper)
        {
            _masterRepo = masterRepo;
            _mapper = mapper;
        }

        public async Task<CreateSingleScheduleResponse> Handle(CreateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            var master = _mapper.Map<DoctorScheduleMaster>(request.DoctorSechedule);

            var created = await _masterRepo.AddAsync(master);
            var result = await _masterRepo.GetByIdAsync(created.Oid, cancellationToken);

            return _mapper.Map<CreateSingleScheduleResponse>(result);
        }
    }
}
