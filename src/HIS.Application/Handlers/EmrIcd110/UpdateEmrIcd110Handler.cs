using AutoMapper;
using HIS.Application.Commands.EmrIcd110;
using HIS.Application.DTOs.Emr_Icd110;
using HIS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.EmrIcd110
{
    public sealed class UpdateEmrIcd110Handler : IRequestHandler<UpdateEmrIcd110Command, EmrResponseDto>
    {
        private readonly IEmrRepository repository;
        private readonly IMapper mapper;

        public UpdateEmrIcd110Handler(IEmrRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<EmrResponseDto> Handle(UpdateEmrIcd110Command request, CancellationToken cancellationToken)
        {
            var emr = await repository.GetByIdAsync(request.Emr.Oid,cancellationToken);
            if (emr is null)
            {
                throw new KeyNotFoundException($"Emr with Key {request.Emr.Oid} NotFound");
            }
            await repository.UpdateAsync(emr,cancellationToken);
            return mapper.Map<EmrResponseDto>(emr);
        }
    }
}
