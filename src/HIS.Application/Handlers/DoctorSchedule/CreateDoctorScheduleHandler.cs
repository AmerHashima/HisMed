using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Interfaces;
using MediatR;
using HIS.Domain.Entities;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class DoctorScheduleHandler : IRequestHandler<CreateDoctorScheduleCommand, DoctorScheduleMasterDetailDto>
    {
        private readonly IDoctorScheduleMasterRepository _masterRepo;
        private readonly IMapper _mapper;

        public DoctorScheduleHandler(IDoctorScheduleMasterRepository masterRepo, IMapper mapper)
        {
            _masterRepo = masterRepo;
            _mapper = mapper;
        }

        public async Task<DoctorScheduleMasterDetailDto> Handle(CreateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            var master = _mapper.Map<DoctorScheduleMaster>(request.DoctorSechedule);

            var created = await _masterRepo.AddAsync(master);

            return _mapper.Map<DoctorScheduleMasterDetailDto>(created);
        }
    }
}
