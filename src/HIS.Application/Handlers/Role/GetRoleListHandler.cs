using AutoMapper;
using HIS.Application.DTOs.Role;
using HIS.Application.Queries.Role;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Role;

public class GetRoleListHandler : IRequestHandler<GetRoleListQuery, IEnumerable<RoleDto>>
{
    private readonly IRoleRepository _repository;
    private readonly IMapper _mapper;

    public GetRoleListHandler(IRoleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleDto>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        var roles = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }
}