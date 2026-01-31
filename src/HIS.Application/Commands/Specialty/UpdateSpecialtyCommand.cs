using HIS.Application.DTOs.Specialty;
using MediatR;

namespace HIS.Application.Commands.Specialty;

public record UpdateSpecialtyCommand(UpdateSpecialtyDto Specialty) : IRequest<SpecialtyDto>;