using AutoMapper;
using FluentValidation;
using HIS.Application.Commands.Diagnosis;
using HIS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.Diagnosis
{
    public sealed class DeleteDiagnosesHandler : IRequestHandler<DeleteDiagnosisCommand, bool>
    {
        private readonly IDiagonsisRepository repository;

        public DeleteDiagnosesHandler( IDiagonsisRepository repository)
        {
            this.repository = repository;
        }
        public async Task<bool> Handle(DeleteDiagnosisCommand request, CancellationToken cancellationToken)
        {
            var diagonsis = await repository.GetByIdAsync(request.Oid);
            if (diagonsis is null)
            {
                return false;
            }
            await repository.DeleteAsync(request.Oid);
            return true;
        }
    }
}
