using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class DoctorScheduleHandler : IRequestHandler<CreateDoctorScheduleCommand, DoctorScheduleDto>
    {
        private readonly IDoctorScheduleRepository doctorScheduleRepo;
        private readonly IMapper mapper;

        public DoctorScheduleHandler(IDoctorScheduleRepository doctorScheduleRepo,IMapper mapper)
        {
            this.doctorScheduleRepo = doctorScheduleRepo;
            this.mapper = mapper;
        }
        public async Task<DoctorScheduleDto> Handle(CreateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            var DoctorSchedule = mapper.Map<Domain.Entities.DoctorSchedule>(request.DoctorSechedule);
           var CreateDoctorSchedule= await doctorScheduleRepo.AddAsync(DoctorSchedule);
            return mapper.Map<DoctorScheduleDto>(CreateDoctorSchedule);
        }
    }
}
