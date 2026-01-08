using AutoMapper;
using HIS.Application.DTOs.SystemUser;
using HIS.Application.Queries.SystemUser;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.SystemUser;

public class GetSystemUserListHandler : IRequestHandler<GetSystemUserListQuery, IEnumerable<SystemUserDto>>
{
    private readonly ISystemUserRepository _repository;
    private readonly IMapper _mapper;

    public GetSystemUserListHandler(ISystemUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SystemUserDto>> Handle(GetSystemUserListQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.SystemUser> users;

        if (request.RoleId.HasValue)
        {
            users = await _repository.GetUsersByRoleAsync(request.RoleId.Value, cancellationToken);
        }
        else if (!request.IncludeInactive)
        {
            users = await _repository.GetActiveUsersAsync(cancellationToken);
        }
        else
        {
            users = await _repository.GetAllAsync(cancellationToken);
        }

        return _mapper.Map<IEnumerable<SystemUserDto>>(users);
    }
}