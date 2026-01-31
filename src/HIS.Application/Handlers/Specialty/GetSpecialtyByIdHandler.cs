using AutoMapper;
using HIS.Application.DTOs.Specialty;
using HIS.Application.Queries.Specialty;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Specialty;

public class GetSpecialtyByIdHandler : IRequestHandler<GetSpecialtyByIdQuery, SpecialtyDto?>
{
    private readonly ISpecialtyRepository _repository;
    private readonly IMapper _mapper;

    public GetSpecialtyByIdHandler(ISpecialtyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<SpecialtyDto?> Handle(GetSpecialtyByIdQuery request, CancellationToken cancellationToken)
    {
        var specialty = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return specialty == null ? null : _mapper.Map<SpecialtyDto>(specialty);
    }
}