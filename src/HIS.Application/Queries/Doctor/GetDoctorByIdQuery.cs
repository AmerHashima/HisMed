using HIS.Application.DTOs.Doctor;
using MediatR;

namespace HIS.Application.Queries.Doctor;

public record GetDoctorByIdQuery(Guid Id) : IRequest<DoctorDto?>;