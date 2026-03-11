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
    public sealed class GetDiagnosisByIdHandler : IRequestHandler<GetDiagnosisByIdQuery, DiagnosisDto>
    {
        private readonly IMapper mapper;
        private readonly IDiagonsisRepository repos;

        public GetDiagnosisByIdHandler(IMapper mapper,IDiagonsisRepository repos)
        {
            this.mapper = mapper;
            this.repos = repos;
        }
        public async Task<DiagnosisDto> Handle(GetDiagnosisByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repos.GetByIdAsync(request.Id);
            if (result is null)
                throw new KeyNotFoundException($"Key {request.Id} NotFound");
            return mapper.Map<DiagnosisDto>(result);
        }
    }
}
