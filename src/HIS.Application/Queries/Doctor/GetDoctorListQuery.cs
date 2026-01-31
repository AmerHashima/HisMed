using HIS.Application.DTOs.Doctor;
using MediatR;

namespace HIS.Application.Queries.Doctor;

public record GetDoctorListQuery(
    bool ActiveOnly = true,
    Guid? SpecialtyId = null,
    Guid? BranchId = null
) : IRequest<IEnumerable<DoctorDto>>;