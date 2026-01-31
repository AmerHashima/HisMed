using AutoMapper;
using HIS.Application.DTOs.Role;
using HIS.Application.Queries.Role;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Role;

public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly IRoleRepository _repository;
    private readonly IMapper _mapper;

    public GetRoleByIdHandler(IRoleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return role == null ? null : _mapper.Map<RoleDto>(role);
    }
}