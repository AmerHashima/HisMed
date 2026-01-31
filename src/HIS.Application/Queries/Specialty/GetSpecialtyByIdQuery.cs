using HIS.Application.DTOs.Specialty;
using MediatR;

namespace HIS.Application.Queries.Specialty;

public record GetSpecialtyByIdQuery(Guid Id) : IRequest<SpecialtyDto?>;