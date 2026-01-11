using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Queries.Patient;

public record GetPatientListQuery(
    string? SearchTerm = null,
    bool IncludeInactive = false,
    char? Gender = null,
    string? BloodGroup = null,
    string? Nationality = null,
    int Page = 1,
    int PageSize = 50
) : IRequest<IEnumerable<PatientDto>>;