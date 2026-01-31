using AutoMapper;
using HIS.Application.Commands.Specialty;
using HIS.Application.DTOs.Specialty;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Specialty;

public class CreateSpecialtyHandler : IRequestHandler<CreateSpecialtyCommand, SpecialtyDto>
{
    private readonly ISpecialtyRepository _repository;
    private readonly IMapper _mapper;

    public CreateSpecialtyHandler(ISpecialtyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<SpecialtyDto> Handle(CreateSpecialtyCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.SpecialtyCodeExistsAsync(request.Specialty.Code, cancellationToken: cancellationToken))
        {
            throw new InvalidOperationException($"Specialty with code '{request.Specialty.Code}' already exists");
        }

        var specialty = _mapper.Map<Domain.Entities.Specialty>(request.Specialty);
        var createdSpecialty = await _repository.AddAsync(specialty, cancellationToken);
        
        return _mapper.Map<SpecialtyDto>(createdSpecialty);
    }
}