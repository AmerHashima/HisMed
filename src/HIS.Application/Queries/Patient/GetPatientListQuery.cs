using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Queries.Patient;

public record GetPatientListQuery(
    string? SearchTerm = null,
    bool IncludeInactive = false,
    Guid? GenderLookupId = null,
    Guid? BloodGroupLookupId = null,
    Guid? NationalityLookupId = null,
    Guid? BranchId = null,
    int Page = 1,
    int PageSize = 50
) : IRequest<IEnumerable<PatientDto>>;