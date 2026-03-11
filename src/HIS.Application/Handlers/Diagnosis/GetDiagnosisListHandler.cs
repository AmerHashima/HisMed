using AutoMapper;
using HIS.Application.DTOs.Diagnosis;
using HIS.Application.Queries.Diagnosis;
using HIS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.Diagnosis
{
    public sealed record GetDiagnosisListHandler : IRequestHandler<GetDiagnosisListQuery, IEnumerable<DiagnosisDto>>
    {
        private readonly IMapper mapper;
        private readonly IDiagonsisRepository repository;

        public GetDiagnosisListHandler(IMapper mapper,IDiagonsisRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<IEnumerable<DiagnosisDto>> Handle(GetDiagnosisListQuery request, CancellationToken cancellationToken)
        {
            if (request.EncounterId.HasValue)
            {
                var result = await repository.GetDiagnosesByEncounterIdAsync(request.EncounterId.Value);
                return mapper.Map<IEnumerable<DiagnosisDto>>(result);
                
            }
            var dgns = await repository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<DiagnosisDto>>(dgns);
        }
    }
}
