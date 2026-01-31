using HIS.Application.DTOs.Encounter;
using HIS.Application.DTOs.Prescription;
using MediatR;

namespace HIS.Application.Commands.Prescription;

public record CreatePrescriptionCommand(CreatePrescriptionDto Prescription) : IRequest<PrescriptionDto>;