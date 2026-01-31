using HIS.Application.Commands.Specialty;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Specialty;

public class DeleteSpecialtyHandler : IRequestHandler<DeleteSpecialtyCommand, bool>
{
    private readonly ISpecialtyRepository _repository;

    public DeleteSpecialtyHandler(ISpecialtyRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteSpecialtyCommand request, CancellationToken cancellationToken)
    {
        var specialty = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (specialty == null)
        {
            return false;
        }

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}