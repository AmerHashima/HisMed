using HIS.Application.Commands.EmrIcd110;
using HIS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.EmrIcd110
{
    public sealed class DeleteEMrIcd110Handler : IRequestHandler<DeleteEmrIcd110Command, bool>
    {
        private readonly IEmrRepository repository;

        public DeleteEMrIcd110Handler(IEmrRepository repository)
        {
            this.repository = repository;
        }
        public async Task<bool> Handle(DeleteEmrIcd110Command request, CancellationToken cancellationToken)
        {
            var emr = await repository.GetByIdAsync(request.Id,cancellationToken);
            if (emr is null)
            {
                return false;
            }
            await repository.DeleteAsync(request.Id,cancellationToken);
            return true;
        }
    }
}
