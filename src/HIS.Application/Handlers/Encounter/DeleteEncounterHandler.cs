using HIS.Application.Commands.Encounter;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Encounter;

public class DeleteEncounterHandler : IRequestHandler<DeleteEncounterCommand, bool>
{
    private readonly IEncounterRepository _repository;

    public DeleteEncounterHandler(IEncounterRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteEncounterCommand request, CancellationToken cancellationToken)
    {
        var encounter = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (encounter == null)
        {
            return false;
        }

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}