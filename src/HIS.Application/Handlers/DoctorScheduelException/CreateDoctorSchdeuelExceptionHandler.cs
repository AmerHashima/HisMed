using AutoMapper;
using HIS.Application.Commands.DoctorScheduelException;
using HIS.Application.DTOs.DoctorScheduleException;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.DoctorScheduelException
{
    public sealed class CreateDoctorSchdeuelExceptionHandler : IRequestHandler<CreateDoctorScheduelExceptionCommand, DoctorScheduleExceptionResponseDto>
    {
        private readonly IMapper mapper;
        private readonly IDoctorscheduelExceptionRepository repository;

        public CreateDoctorSchdeuelExceptionHandler(IMapper mapper,IDoctorscheduelExceptionRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<DoctorScheduleExceptionResponseDto> Handle(CreateDoctorScheduelExceptionCommand request, CancellationToken cancellationToken)
        {
            var DoctorException = mapper.Map<Domain.Entities.DoctorScheduleException>(request.DoctorScheduelException);
            var exception = await repository.AddAsync(DoctorException);
            var result = await repository.GetByIdAsync(exception.Oid); 
            return mapper.Map<DoctorScheduleExceptionResponseDto>(result);
        }
    }
}
