using HIS.Application.DTOs.Doctor;
using MediatR;

namespace HIS.Application.Commands.Doctor;

public record CreateDoctorCommand(CreateDoctorDto Doctor) : IRequest<DoctorDto>;