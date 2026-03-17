using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class UpdateDoctorScheduelHandler : IRequestHandler<UpdateDoctorScheduleCommand, DoctorScheduleDto>
    {
        private readonly IMapper _mapper;
        private readonly IDoctorScheduleMasterRepository _doctorScheduleRepo;

        public UpdateDoctorScheduelHandler(IMapper mapper, IDoctorScheduleMasterRepository doctorScheduleRepo)
        {
            _mapper = mapper;
            _doctorScheduleRepo = doctorScheduleRepo;
        }

        public async Task<DoctorScheduleDto> Handle(UpdateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            var master = await _doctorScheduleRepo.GetByIdAsync(request.DoctorSchdeuel.Oid, cancellationToken);
            if (master == null)
            {
                throw new KeyNotFoundException($"Doctor schedule with ID {request.DoctorSchdeuel.Oid} not found");
            }

            // Update master-level properties
            _mapper.Map(request.DoctorSchdeuel, master);

            // Update detail-level properties
            var existingDetail = master.Details.FirstOrDefault();
            if (existingDetail == null) { throw new KeyNotFoundException($"No Details  Found For Schedule Id {request.DoctorSchdeuel.Oid}"); }
            existingDetail.StartTime = request.DoctorSchdeuel.StartTime;
            existingDetail.EndTime = request.DoctorSchdeuel.EndTime;
            existingDetail.SlotDurationMinutes = request.DoctorSchdeuel.SlotDurationMinutes;
            existingDetail.DayOfWeekId = request.DoctorSchdeuel.DayOfWeekId;

            await _doctorScheduleRepo.UpdateAsync(master);

            return _mapper.Map<DoctorScheduleDto>(master);
        }
    }
}
