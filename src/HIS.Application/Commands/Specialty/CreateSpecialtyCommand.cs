using HIS.Application.DTOs.Specialty;
using MediatR;

namespace HIS.Application.Commands.Specialty;

public record CreateSpecialtyCommand(CreateSpecialtyDto Specialty) : IRequest<SpecialtyDto>;