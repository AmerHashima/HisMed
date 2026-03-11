using AutoMapper;
using HIS.Application.DTOs.Emr_Icd110;
using HIS.Application.Mappings;
using HIS.Application.Queries.EmrIcd110;
using HIS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.EmrIcd110
{
    
    public class GetEmrIcd110ListHandler : IRequestHandler<GetEmrIcd110ListQuery, IEnumerable<EmrResponseDto>>
    {
        private readonly IEmrRepository repository;
        private readonly IMapper mapper;

        public GetEmrIcd110ListHandler(IEmrRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<EmrResponseDto>> Handle(GetEmrIcd110ListQuery request, CancellationToken cancellationToken)
        {
            if (request.AustCode.HasValue)
            {
                var emr = await repository.GetEmrByAustCodeAsync(request.AustCode.Value,cancellationToken);
                return mapper.Map<IEnumerable<EmrResponseDto>>(emr);
            }
            else if (request.Level.HasValue)
            {
                var emr = await repository.GetEmrByLevelAsync(request.Level.Value,cancellationToken);
                return mapper.Map<IEnumerable<EmrResponseDto>>(emr);
            }
            else if (request.CodeId == null)
            {
                var emr = await repository.GetEmrByCodeIdAsync(request.CodeId,cancellationToken);
                return mapper.Map<IEnumerable<EmrResponseDto>>(emr);
            }
            else if (request.Sex.HasValue)
            {
                var emr = await repository.GetEmrBySexAsync(request.Sex.Value, cancellationToken);
                return mapper.Map<IEnumerable<EmrResponseDto>>(emr);
            }
           var result = await repository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<EmrResponseDto>>(result);

        }
    }
}
