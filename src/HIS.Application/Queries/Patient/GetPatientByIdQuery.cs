using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Queries.Patient;

public record GetPatientByIdQuery(Guid Id) : IRequest<PatientDto?>;