using AutoMapper;
using HIS.Application.DTOs.DoctorScheduleException;
using HIS.Application.Queries.DoctorSceduelException;
using HIS.Domain.Interfaces;
using MediatR;
namespace HIS.Application.Handlers.DoctorScheduelException
{
    public sealed class GetDoctorScheduelExceptionByIdHandler : IRequestHandler<GetDoctorScheduelExceptionByIdQuery, DoctorScheduleExceptionResponseDto?>
    {
        private readonly IMapper mapper;
        private readonly IDoctorscheduelExceptionRepository repository;

        public GetDoctorScheduelExceptionByIdHandler(IMapper mapper, IDoctorscheduelExceptionRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<DoctorScheduleExceptionResponseDto?> Handle(GetDoctorScheduelExceptionByIdQuery request, CancellationToken cancellationToken)
        {
            var Schdeuel = await repository.GetByIdAsync(request.id);

            return Schdeuel == null ? null : mapper.Map<DoctorScheduleExceptionResponseDto>(Schdeuel);
        }
    }
}
