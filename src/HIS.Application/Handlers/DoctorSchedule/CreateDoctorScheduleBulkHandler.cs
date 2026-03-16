using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class CreateDoctorScheduleBulkHandler : IRequestHandler<CreateDoctorScheduleBulkCommand, List<GetDoctorScheduleMasterAndDetailDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDoctorScheduleMasterRepository _repository;

        public CreateDoctorScheduleBulkHandler(IMapper mapper, IDoctorScheduleMasterRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<GetDoctorScheduleMasterAndDetailDto>> Handle(CreateDoctorScheduleBulkCommand request, CancellationToken cancellationToken)
        {
            var scheduleEntities = request.DoctorSechduelList
                .SelectMany(bulk => bulk.DoctorSchedules
                    .Select(dto => _mapper.Map<DoctorScheduleMaster>(dto)));

            var result = await _repository.AddDoctorScheduelList(scheduleEntities, cancellationToken);

            return _mapper.Map<List<GetDoctorScheduleMasterAndDetailDto>>(result);
        }
    }
}
