using AutoMapper;
using HIS.Application.Commands.Diagnosis;
using HIS.Application.DTOs.Diagnosis;
using HIS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.Diagnosis
{
    public sealed class UpdateDiagonsisHandler : IRequestHandler<UpdateDiagnosisCommand, DiagnosisDto>
    {
        private readonly IMapper mapper;
        private readonly IEncounterRepository encountrrepository;
        private readonly IDiagonsisRepository repository;

        public UpdateDiagonsisHandler(IMapper mapper,IEncounterRepository Encountrrepository,IDiagonsisRepository repository)
        {
            this.mapper = mapper;
            encountrrepository = Encountrrepository;
            this.repository = repository;
        }
        public async Task<DiagnosisDto> Handle(UpdateDiagnosisCommand request, CancellationToken cancellationToken)
        {
            var diagonsis = await repository.GetByIdAsync(request.Diagonsis.Oid);
            if (diagonsis is null)
            {
                throw new KeyNotFoundException($"Diagonsis with key{request.Diagonsis.Oid} NotFound");
            }
            await repository.UpdateAsync(diagonsis);
            return mapper.Map<DiagnosisDto>(diagonsis);
        }
    }
}
