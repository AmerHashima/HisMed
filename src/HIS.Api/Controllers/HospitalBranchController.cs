using HIS.Api.Models;
using HIS.Application.Commands.HospitalBranch;
using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.HospitalBranch;
using HIS.Application.Queries.HospitalBranch;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class HospitalBranchController : BaseApiController
{
    private readonly IMediator _mediator;

    public HospitalBranchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get hospital branch data with advanced filtering, sorting, and pagination
    /// </summary>
    /// <param name="request">Query request with filters, sorting, and pagination</param>
    /// <returns>Paginated hospital branch data</returns>
    [HttpPost("query")]
    public async Task<ActionResult<ApiResponse<PagedResult<HospitalBranchDto>>>> GetBranchData([FromBody] QueryRequest request)
    {
        try
        {
            var query = new GetHospitalBranchDataQuery(request);
            var result = await _mediator.Send(query);
            return SuccessResponse(result, "Branch data retrieved successfully");
        }
        catch (Exception ex)
        {
            return ErrorResponse<PagedResult<HospitalBranchDto>>($"Error retrieving branch data: {ex.Message}", 500);
        }
    }

    /// <summary>
    /// Get all hospital branches
    /// </summary>
    /// <param name="activeOnly">Return only active branches</param>
    /// <returns>List of hospital branches</returns>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<HospitalBranchDto>>>> GetBranches([FromQuery] bool activeOnly = true)
    {
        var query = new GetHospitalBranchListQuery(activeOnly);
        var branches = await _mediator.Send(query);
        return SuccessResponse(branches, "Branches retrieved successfully");
    }

    /// <summary>
    /// Get hospital branch by ID
    /// </summary>
    /// <param name="id">Branch ID</param>
    /// <returns>Branch details</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<HospitalBranchDto>>> GetBranch(Guid id)
    {
        var query = new GetHospitalBranchByIdQuery(id);
        var branch = await _mediator.Send(query);

        if (branch == null)
            return ErrorResponse<HospitalBranchDto>("Branch not found", 404);

        return SuccessResponse(branch, "Branch retrieved successfully");
    }

    /// <summary>
    /// Create a new hospital branch
    /// </summary>
    /// <param name="createBranchDto">Branch creation data</param>
    /// <returns>Created branch</returns>
    [HttpPost]
    
    public async Task<ActionResult<ApiResponse<HospitalBranchDto>>> CreateBranch([FromBody] CreateHospitalBranchDto createBranchDto)
    {
        try
        {
            var command = new CreateHospitalBranchCommand(createBranchDto);
            var branch = await _mediator.Send(command);
            return CreatedResponse(branch, nameof(GetBranch), new { id = branch.Oid }, "Branch created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<HospitalBranchDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Update an existing hospital branch
    /// </summary>
    /// <param name="id">Branch ID</param>
    /// <param name="updateBranchDto">Branch update data</param>
    /// <returns>Updated branch</returns>
    [HttpPut("{id}")]
    
    public async Task<ActionResult<ApiResponse<HospitalBranchDto>>> UpdateBranch(Guid id, [FromBody] UpdateHospitalBranchDto updateBranchDto)
    {
        try
        {
            if (id != updateBranchDto.Oid)
                return ErrorResponse<HospitalBranchDto>("Branch ID mismatch", 400);

            var command = new UpdateHospitalBranchCommand(updateBranchDto);
            var branch = await _mediator.Send(command);
            return SuccessResponse(branch, "Branch updated successfully");
        }
        catch (KeyNotFoundException)
        {
            return ErrorResponse<HospitalBranchDto>("Branch not found", 404);
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<HospitalBranchDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Delete a hospital branch (soft delete)
    /// </summary>
    /// <param name="id">Branch ID</param>
    /// <returns>Success response</returns>
    [HttpDelete("{id}")]
    
    public async Task<ActionResult<ApiResponse>> DeleteBranch(Guid id)
    {
        try
        {
            var command = new DeleteHospitalBranchCommand(id);
            var result = await _mediator.Send(command);

            if (!result)
                return ErrorResponse("Branch not found", 404);

            return SuccessResponse("Branch deleted successfully");
        }
        catch (Exception ex)
        {
            return ErrorResponse($"Error deleting branch: {ex.Message}", 500);
        }
    }
}