using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Queries.Patient;

public record GetPatientByIdentityNumberQuery(string IdentityNumber) : IRequest<PatientDto?>;