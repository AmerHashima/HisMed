using HIS.Application.DTOs.Specialty;
using MediatR;

namespace HIS.Application.Queries.Specialty;

public record GetSpecialtyListQuery(bool ActiveOnly = true) : IRequest<IEnumerable<SpecialtyDto>>;