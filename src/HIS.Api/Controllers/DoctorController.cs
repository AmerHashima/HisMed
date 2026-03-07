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

    #region Doctor Branches

    /// <summary>
    /// Get all branches for a doctor
    /// </summary>
    [HttpGet("{doctorId}/branches")]
    public async Task<ActionResult<ApiResponse<IEnumerable<DoctorBranchDto>>>> GetDoctorBranches(Guid doctorId)
    {
        var query = new GetDoctorBranchesQuery(doctorId);
        var branches = await _mediator.Send(query);
        return SuccessResponse(branches, "Doctor branches retrieved successfully");
    }

    /// <summary>
    /// Add a branch to a doctor
    /// </summary>
    [HttpPost("{doctorId}/branches")]
    public async Task<ActionResult<ApiResponse<DoctorBranchDto>>> CreateDoctorBranch(Guid doctorId, [FromBody] CreateDoctorBranchDto dto)
    {
        try
        {
            dto.DoctorId = doctorId;
            var command = new CreateDoctorBranchCommand(dto);
            var branch = await _mediator.Send(command);
            return SuccessResponse(branch, "Doctor branch created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<DoctorBranchDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Delete a doctor branch (soft delete)
    /// </summary>
    [HttpDelete("branches/{id}")]
    public async Task<ActionResult<ApiResponse>> DeleteDoctorBranch(Guid id)
    {
        var command = new DeleteDoctorBranchCommand(id);
        var result = await _mediator.Send(command);

        if (!result)
            return ErrorResponse("Doctor branch not found", 404);

        return SuccessResponse("Doctor branch deleted successfully");
    }

    #endregion

    #region Doctor Attachments

    /// <summary>
    /// Get all attachments for a doctor
    /// </summary>
    [HttpGet("{doctorId}/attachments")]
    public async Task<ActionResult<ApiResponse<IEnumerable<DoctorAttachmentDto>>>> GetDoctorAttachments(Guid doctorId)
    {
        var query = new GetDoctorAttachmentsQuery(doctorId);
        var attachments = await _mediator.Send(query);
        return SuccessResponse(attachments, "Doctor attachments retrieved successfully");
    }

    /// <summary>
    /// Add an attachment to a doctor
    /// </summary>
    [HttpPost("{doctorId}/attachments")]
    public async Task<ActionResult<ApiResponse<DoctorAttachmentDto>>> CreateDoctorAttachment(Guid doctorId, [FromBody] CreateDoctorAttachmentDto dto)
    {
        try
        {
            dto.DoctorId = doctorId;
            var command = new CreateDoctorAttachmentCommand(dto);
            var attachment = await _mediator.Send(command);
            return SuccessResponse(attachment, "Doctor attachment created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<DoctorAttachmentDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Delete a doctor attachment (soft delete)
    /// </summary>
    [HttpDelete("attachments/{id}")]
    public async Task<ActionResult<ApiResponse>> DeleteDoctorAttachment(Guid id)
    {
        var command = new DeleteDoctorAttachmentCommand(id);
        var result = await _mediator.Send(command);

        if (!result)
            return ErrorResponse("Doctor attachment not found", 404);

        return SuccessResponse("Doctor attachment deleted successfully");
    }

    #endregion
}