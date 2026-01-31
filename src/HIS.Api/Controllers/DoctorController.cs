using HIS.Api.Models;
using HIS.Application.Commands.Doctor;
using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.Doctor;
using HIS.Application.Queries.Doctor;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class DoctorController : BaseApiController
{
    private readonly IMediator _mediator;

    public DoctorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get doctor data with advanced filtering, sorting, and pagination
    /// </summary>
    [HttpPost("query")]
    public async Task<ActionResult<ApiResponse<PagedResult<DoctorDto>>>> GetDoctorData([FromBody] QueryRequest request)
    {
        try
        {
            var query = new GetDoctorDataQuery(request);
            var result = await _mediator.Send(query);
            return SuccessResponse(result, "Doctor data retrieved successfully");
        }
        catch (Exception ex)
        {
            return ErrorResponse<PagedResult<DoctorDto>>($"Error retrieving doctor data: {ex.Message}", 500);
        }
    }

    /// <summary>
    /// Get all doctors with optional filtering
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<DoctorDto>>>> GetDoctors(
        [FromQuery] bool activeOnly = true,
        [FromQuery] Guid? specialtyId = null,
        [FromQuery] Guid? branchId = null)
    {
        var query = new GetDoctorListQuery(activeOnly, specialtyId, branchId);
        var doctors = await _mediator.Send(query);
        return SuccessResponse(doctors, "Doctors retrieved successfully");
    }

    /// <summary>
    /// Get doctor by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<DoctorDto>>> GetDoctor(Guid id)
    {
        var query = new GetDoctorByIdQuery(id);
        var doctor = await _mediator.Send(query);

        if (doctor == null)
            return ErrorResponse<DoctorDto>("Doctor not found", 404);

        return SuccessResponse(doctor, "Doctor retrieved successfully");
    }

    /// <summary>
    /// Create a new doctor
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<DoctorDto>>> CreateDoctor([FromBody] CreateDoctorDto createDoctorDto)
    {
        try
        {
            var command = new CreateDoctorCommand(createDoctorDto);
            var doctor = await _mediator.Send(command);
            return CreatedResponse(doctor, nameof(GetDoctor), new { id = doctor.Oid }, "Doctor created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<DoctorDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Update an existing doctor
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<DoctorDto>>> UpdateDoctor(Guid id, [FromBody] UpdateDoctorDto updateDoctorDto)
    {
        try
        {
            if (id != updateDoctorDto.Oid)
                return ErrorResponse<DoctorDto>("Doctor ID mismatch", 400);

            var command = new UpdateDoctorCommand(updateDoctorDto);
            var doctor = await _mediator.Send(command);
            return SuccessResponse(doctor, "Doctor updated successfully");
        }
        catch (KeyNotFoundException)
        {
            return ErrorResponse<DoctorDto>("Doctor not found", 404);
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<DoctorDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Delete a doctor (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse>> DeleteDoctor(Guid id)
    {
        try
        {
            var command = new DeleteDoctorCommand(id);
            var result = await _mediator.Send(command);

            if (!result)
                return ErrorResponse("Doctor not found", 404);

            return SuccessResponse("Doctor deleted successfully");
        }
        catch (Exception ex)
        {
            return ErrorResponse($"Error deleting doctor: {ex.Message}", 500);
        }
    }
}