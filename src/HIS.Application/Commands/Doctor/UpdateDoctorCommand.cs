using HIS.Application.DTOs.Doctor;
using MediatR;

namespace HIS.Application.Commands.Doctor;

public record UpdateDoctorCommand(UpdateDoctorDto Doctor) : IRequest<DoctorDto>;