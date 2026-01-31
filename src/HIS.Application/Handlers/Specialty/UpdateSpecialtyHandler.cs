using AutoMapper;
using HIS.Application.Commands.Specialty;
using HIS.Application.DTOs.Specialty;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Specialty;

public class UpdateSpecialtyHandler : IRequestHandler<UpdateSpecialtyCommand, SpecialtyDto>
{
    private readonly ISpecialtyRepository _repository;
    private readonly IMapper _mapper;

    public UpdateSpecialtyHandler(ISpecialtyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<SpecialtyDto> Handle(UpdateSpecialtyCommand request, CancellationToken cancellationToken)
    {
        var existingSpecialty = await _repository.GetByIdAsync(request.Specialty.Oid, cancellationToken);
        if (existingSpecialty == null)
        {
            throw new KeyNotFoundException($"Specialty with ID {request.Specialty.Oid} not found");
        }

        if (await _repository.SpecialtyCodeExistsAsync(request.Specialty.Code, request.Specialty.Oid, cancellationToken))
        {
            throw new InvalidOperationException($"Specialty code '{request.Specialty.Code}' is already in use");
        }

        existingSpecialty.Code = request.Specialty.Code;
        existingSpecialty.NameAr = request.Specialty.NameAr;
        existingSpecialty.NameEn = request.Specialty.NameEn;
        existingSpecialty.DefaultVisitDuration = request.Specialty.DefaultVisitDuration;
        existingSpecialty.DefaultPrice = request.Specialty.DefaultPrice;
        existingSpecialty.IsActive = request.Specialty.IsActive;
        existingSpecialty.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existingSpecialty, cancellationToken);
        return _mapper.Map<SpecialtyDto>(existingSpecialty);
    }
}