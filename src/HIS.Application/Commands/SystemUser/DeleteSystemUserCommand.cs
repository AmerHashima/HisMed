using MediatR;

namespace HIS.Application.Commands.SystemUser;

public class DeleteSystemUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}