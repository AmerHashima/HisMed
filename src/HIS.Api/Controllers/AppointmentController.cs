using HIS.Api.Models;
using HIS.Application.Commands.Appointment;
using HIS.Application.DTOs.Appointment;
using HIS.Application.Queries.Appointment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class AppointmentController : BaseApiController
{
    private readonly IMediator _mediator;

    public AppointmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get appointments with optional filtering
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<AppointmentDto>>>> GetAppointments(
        [FromQuery] Guid? patientId = null,
        [FromQuery] Guid? doctorId = null,
        [FromQuery] DateTime? date = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] string? status = null)
    {
        var query = new GetAppointmentListQuery(patientId, doctorId, date, startDate, endDate, status);
        var appointments = await _mediator.Send(query);
        return SuccessResponse(appointments, "Appointments retrieved successfully");
    }

    /// <summary>
    /// Get appointment by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<AppointmentDto>>> GetAppointment(Guid id)
    {
        var query = new GetAppointmentByIdQuery(id);
        var appointment = await _mediator.Send(query);

        if (appointment == null)
            return ErrorResponse<AppointmentDto>("Appointment not found", 404);

        return SuccessResponse(appointment, "Appointment retrieved successfully");
    }

    /// <summary>
    /// Create a new appointment
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<AppointmentDto>>> CreateAppointment([FromBody] CreateAppointmentDto createAppointmentDto)
    {
        try
        {
            var command = new CreateAppointmentCommand(createAppointmentDto);
            var appointment = await _mediator.Send(command);
            return CreatedResponse(appointment, nameof(GetAppointment), new { id = appointment.Oid }, "Appointment created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<AppointmentDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Update an existing appointment
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<AppointmentDto>>> UpdateAppointment(Guid id, [FromBody] UpdateAppointmentDto updateAppointmentDto)
    {
        try
        {
            if (id != updateAppointmentDto.Oid)
                return ErrorResponse<AppointmentDto>("Appointment ID mismatch", 400);

            var command = new UpdateAppointmentCommand(updateAppointmentDto);
            var appointment = await _mediator.Send(command);
            return SuccessResponse(appointment, "Appointment updated successfully");
        }
        catch (KeyNotFoundException)
        {
            return ErrorResponse<AppointmentDto>("Appointment not found", 404);
        }
    }

    /// <summary>
    /// Cancel an appointment
    /// </summary>
    [HttpPost("{id}/cancel")]
    public async Task<ActionResult<ApiResponse>> CancelAppointment(Guid id, [FromQuery] string? reason = null)
    {
        try
        {
            var command = new CancelAppointmentCommand(id, reason);
            var result = await _mediator.Send(command);

            if (!result)
                return ErrorResponse("Appointment not found", 404);

            return SuccessResponse("Appointment cancelled successfully");
        }
        catch (Exception ex)
        {
            return ErrorResponse($"Error cancelling appointment: {ex.Message}", 500);
        }
    }
}