using AutoMapper;
using HIS.Application.DTOs.SystemUser;
using HIS.Application.Queries.SystemUser;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.SystemUser;

public class GetSystemUserByIdHandler : IRequestHandler<GetSystemUserByIdQuery, SystemUserDto?>
{
    private readonly ISystemUserRepository _repository;
    private readonly IMapper _mapper;

    public GetSystemUserByIdHandler(ISystemUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<SystemUserDto?> Handle(GetSystemUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return user != null ? _mapper.Map<SystemUserDto>(user) : null;
    }
}