using AutoMapper;
using HIS.Application.Commands.Appointment;
using HIS.Application.DTOs.Appointment;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Appointment;

public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, AppointmentDto>
{
    private readonly IAppointmentRepository _repository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public CreateAppointmentHandler(
        IAppointmentRepository repository,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        IMapper mapper)
    {
        _repository = repository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<AppointmentDto> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        // Validate that the patient exists
        var patient = await _patientRepository.GetByIdAsync(request.Appointment.PatientId, cancellationToken);
        if (patient == null)
        {
            throw new InvalidOperationException($"Patient with ID '{request.Appointment.PatientId}' not found");
        }

        // Validate that the doctor exists
        var doctor = await _doctorRepository.GetByIdAsync(request.Appointment.DoctorId, cancellationToken);
        if (doctor == null)
        {
            throw new InvalidOperationException($"Doctor with ID '{request.Appointment.DoctorId}' not found");
        }

        var appointment = _mapper.Map<Domain.Entities.Appointment>(request.Appointment);
        var createdAppointment = await _repository.AddAsync(appointment, cancellationToken);
        
        // Reload with navigation properties
        var appointmentWithDetails = await _repository.GetAppointmentWithDetailsAsync(createdAppointment.Oid, cancellationToken);
        
        return _mapper.Map<AppointmentDto>(appointmentWithDetails);
    }
}
