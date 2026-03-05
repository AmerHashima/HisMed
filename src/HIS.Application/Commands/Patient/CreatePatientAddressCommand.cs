using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Commands.Patient;

public record CreatePatientAddressCommand(CreatePatientAddressDto Address) : IRequest<PatientAddressDto>;
