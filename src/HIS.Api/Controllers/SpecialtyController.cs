using HIS.Api.Models;
using HIS.Application.Commands.Specialty;
using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.Specialty;
using HIS.Application.Queries.Specialty;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class SpecialtyController : BaseApiController
{
    private readonly IMediator _mediator;

    public SpecialtyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get specialty data with advanced filtering, sorting, and pagination
    /// </summary>
    [HttpPost("query")]
    public async Task<ActionResult<ApiResponse<PagedResult<SpecialtyDto>>>> GetSpecialtyData([FromBody] QueryRequest request)
    {
        try
        {
            var query = new GetSpecialtyDataQuery(request);
            var result = await _mediator.Send(query);
            return SuccessResponse(result, "Specialty data retrieved successfully");
        }
        catch (Exception ex)
        {
            return ErrorResponse<PagedResult<SpecialtyDto>>($"Error retrieving specialty data: {ex.Message}", 500);
        }
    }

    /// <summary>
    /// Get all specialties
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<SpecialtyDto>>>> GetSpecialties([FromQuery] bool activeOnly = true)
    {
        var query = new GetSpecialtyListQuery(activeOnly);
        var specialties = await _mediator.Send(query);
        return SuccessResponse(specialties, "Specialties retrieved successfully");
    }

    /// <summary>
    /// Get specialty by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<SpecialtyDto>>> GetSpecialty(Guid id)
    {
        var query = new GetSpecialtyByIdQuery(id);
        var specialty = await _mediator.Send(query);

        if (specialty == null)
            return ErrorResponse<SpecialtyDto>("Specialty not found", 404);

        return SuccessResponse(specialty, "Specialty retrieved successfully");
    }

    /// <summary>
    /// Create a new specialty
    /// </summary>
    [HttpPost]
    
    public async Task<ActionResult<ApiResponse<SpecialtyDto>>> CreateSpecialty([FromBody] CreateSpecialtyDto createSpecialtyDto)
    {
        try
        {
            var command = new CreateSpecialtyCommand(createSpecialtyDto);
            var specialty = await _mediator.Send(command);
            return CreatedResponse(specialty, nameof(GetSpecialty), new { id = specialty.Oid }, "Specialty created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<SpecialtyDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Update an existing specialty
    /// </summary>
    [HttpPut("{id}")]
    
    public async Task<ActionResult<ApiResponse<SpecialtyDto>>> UpdateSpecialty(Guid id, [FromBody] UpdateSpecialtyDto updateSpecialtyDto)
    {
        try
        {
            if (id != updateSpecialtyDto.Oid)
                return ErrorResponse<SpecialtyDto>("Specialty ID mismatch", 400);

            var command = new UpdateSpecialtyCommand(updateSpecialtyDto);
            var specialty = await _mediator.Send(command);
            return SuccessResponse(specialty, "Specialty updated successfully");
        }
        catch (KeyNotFoundException)
        {
            return ErrorResponse<SpecialtyDto>("Specialty not found", 404);
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<SpecialtyDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Delete a specialty (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    
    public async Task<ActionResult<ApiResponse>> DeleteSpecialty(Guid id)
    {
        try
        {
            var command = new DeleteSpecialtyCommand(id);
            var result = await _mediator.Send(command);

            if (!result)
                return ErrorResponse("Specialty not found", 404);

            return SuccessResponse("Specialty deleted successfully");
        }
        catch (Exception ex)
        {
            return ErrorResponse($"Error deleting specialty: {ex.Message}", 500);
        }
    }
}