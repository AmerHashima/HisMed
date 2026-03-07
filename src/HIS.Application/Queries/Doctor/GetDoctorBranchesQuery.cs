using HIS.Application.DTOs.Doctor;
using MediatR;

namespace HIS.Application.Queries.Doctor;

public record GetDoctorBranchesQuery(Guid DoctorId) : IRequest<IEnumerable<DoctorBranchDto>>;
