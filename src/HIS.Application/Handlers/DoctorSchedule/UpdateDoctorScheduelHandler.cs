using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class UpdateDoctorScheduelHandler : IRequestHandler<UpdateDoctorScheduleCommand, CreateSingleScheduleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDoctorScheduleMasterRepository _doctorScheduleRepo;

        public UpdateDoctorScheduelHandler(IMapper mapper, IDoctorScheduleMasterRepository doctorScheduleRepo)
        {
            _mapper = mapper;
            _doctorScheduleRepo = doctorScheduleRepo;
        }

        public async Task<CreateSingleScheduleResponse> Handle(UpdateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            var master = await _doctorScheduleRepo.GetByIdAsync(request.DoctorSchdeuel.Oid, cancellationToken);
            if (master == null)
            {
                throw new KeyNotFoundException($"Doctor schedule with ID {request.DoctorSchdeuel.Oid} not found");
            }

            // Update master-level properties only
            _mapper.Map(request.DoctorSchdeuel, master);

            await _doctorScheduleRepo.UpdateAsync(master);

            // Re-fetch with includes for response mapping
            var result = await _doctorScheduleRepo.GetByIdAsync(master.Oid, cancellationToken);

            return _mapper.Map<CreateSingleScheduleResponse>(result);
        }
    }
}
