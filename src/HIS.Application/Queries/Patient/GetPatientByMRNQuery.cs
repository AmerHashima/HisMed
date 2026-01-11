using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Queries.Patient;

public record GetPatientByMRNQuery(string MRN) : IRequest<PatientDto?>;