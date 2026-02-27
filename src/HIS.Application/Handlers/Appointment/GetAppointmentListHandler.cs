using AutoMapper;
using HIS.Application.DTOs.Appointment;
using HIS.Application.Queries.Appointment;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Appointment;

public class GetAppointmentListHandler : IRequestHandler<GetAppointmentListQuery, IEnumerable<AppointmentDto>>
{
    private readonly IAppointmentRepository _repository;
    private readonly IMapper _mapper;

    public GetAppointmentListHandler(IAppointmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppointmentDto>> Handle(GetAppointmentListQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.Appointment> appointments;

        // Filter by patient
        if (request.PatientId.HasValue)
        {
            appointments = await _repository.GetAppointmentsByPatientAsync(request.PatientId.Value, cancellationToken);
        }
        // Filter by doctor and date
        else if (request.DoctorId.HasValue && request.Date.HasValue)
        {
            appointments = await _repository.GetAppointmentsByDoctorAsync(request.DoctorId.Value, request.Date.Value, cancellationToken);
        }
        // Filter by date range
        else if (request.StartDate.HasValue && request.EndDate.HasValue)
        {
            appointments = await _repository.GetAppointmentsByDateRangeAsync(request.StartDate.Value, request.EndDate.Value, cancellationToken);
        }
        // Get all appointments
        else
        {
            appointments = await _repository.GetAllAsync(cancellationToken);
        }

        // Apply status filter if provided
        if (!string.IsNullOrEmpty(request.Status))
        {
            appointments = appointments.Where(a => a.Status == request.Status);
        }

        return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
    }
}
