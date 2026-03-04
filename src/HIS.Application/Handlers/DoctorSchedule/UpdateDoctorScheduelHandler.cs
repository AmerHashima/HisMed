using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Common;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class UpdateDoctorScheduelHandler : IRequestHandler<UpdateDoctorScheduleCommand, DoctorScheduleDto>
    {
        private readonly IMapper mapper;
        private readonly IDoctorScheduleRepository doctorScheduelRepo;

        public UpdateDoctorScheduelHandler(IMapper mapper,IDoctorScheduleRepository DoctorScheduelRepo)
        {
            this.mapper = mapper;
            doctorScheduelRepo = DoctorScheduelRepo;
        }
        public async Task<DoctorScheduleDto> Handle(UpdateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            var existingScheduel = await doctorScheduelRepo.GetByIdAsync(request.DoctorSchdeuel.Oid, cancellationToken);
            if (existingScheduel == null)
            {
                throw new KeyNotFoundException($"Doctor with ID {request.DoctorSchdeuel.Oid} not found");
            }
            
             await doctorScheduelRepo.UpdateAsync(existingScheduel, cancellationToken);
            return mapper.Map< DoctorScheduleDto >(existingScheduel);
        }
    }
}
