using AutoMapper;
using HIS.Application.DTOs.SystemUserSpace;
using HIS.Application.Queries.SystemUserSpace;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.SystemUserSpace;

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
        return user == null ? null : _mapper.Map<SystemUserDto>(user);
    }
}