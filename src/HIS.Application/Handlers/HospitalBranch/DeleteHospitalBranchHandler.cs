using HIS.Application.Commands.HospitalBranch;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.HospitalBranch;

public class DeleteHospitalBranchHandler : IRequestHandler<DeleteHospitalBranchCommand, bool>
{
    private readonly IHospitalBranchRepository _repository;

    public DeleteHospitalBranchHandler(IHospitalBranchRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteHospitalBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (branch == null)
        {
            return false;
        }

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}