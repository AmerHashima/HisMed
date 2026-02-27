using AutoMapper;
using HIS.Application.DTOs.Appointment;
using HIS.Application.Queries.Appointment;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Appointment;

public class GetAppointmentByIdHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto?>
{
    private readonly IAppointmentRepository _repository;
    private readonly IMapper _mapper;

    public GetAppointmentByIdHandler(IAppointmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AppointmentDto?> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        var appointment = await _repository.GetAppointmentWithDetailsAsync(request.Id, cancellationToken);
        return appointment != null ? _mapper.Map<AppointmentDto>(appointment) : null;
    }
}
