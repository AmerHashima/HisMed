using AutoMapper;
using HIS.Application.Commands.EmrIcd110;
using HIS.Application.DTOs.Emr_Icd110;
using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.EmrIcd110
{
    public sealed class CreateEmrIcd110Handler : IRequestHandler<CreateEmrIcd110Command, EmrResponseDto>
    {
        private readonly IEmrRepository repository;
        private readonly IMapper mapper;

        public CreateEmrIcd110Handler(IEmrRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<EmrResponseDto> Handle(CreateEmrIcd110Command request, CancellationToken cancellationToken)
        {
            var emr = mapper.Map<emr_icd110>(request.Emr);
            await repository.AddAsync(emr,cancellationToken);
            return mapper.Map<EmrResponseDto>(emr);
        }
    }
}
