using AutoMapper;
using HIS.Application.Commands.Appointment;
using HIS.Application.DTOs.Appointment;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Appointment;

public class UpdateAppointmentHandler : IRequestHandler<UpdateAppointmentCommand, AppointmentDto>
{
    private readonly IAppointmentRepository _repository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public UpdateAppointmentHandler(
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

    public async Task<AppointmentDto> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _repository.GetByIdAsync(request.Appointment.Oid, cancellationToken);
        if (appointment == null)
        {
            throw new InvalidOperationException($"Appointment with ID '{request.Appointment.Oid}' not found");
        }

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

        // Update properties
        appointment.PatientId = request.Appointment.PatientId;
        appointment.DoctorId = request.Appointment.DoctorId;
        appointment.AppointmentDate = request.Appointment.AppointmentDate;
        appointment.AppointmentType = request.Appointment.AppointmentType;
        appointment.Status = request.Appointment.Status;
        appointment.Reason = request.Appointment.Reason;
        appointment.BranchId = request.Appointment.BranchId;

        await _repository.UpdateAsync(appointment, cancellationToken);

        // Reload with navigation properties
        var appointmentWithDetails = await _repository.GetAppointmentWithDetailsAsync(appointment.Oid, cancellationToken);

        return _mapper.Map<AppointmentDto>(appointmentWithDetails);
    }
}
