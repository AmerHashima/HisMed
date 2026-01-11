using HIS.Api.Models;
using HIS.Application.Commands.AppLookup;
using HIS.Application.DTOs.AppLookup;
using HIS.Application.DTOs.Common;
using HIS.Application.Queries.AppLookup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class AppLookupController : BaseApiController
{
    private readonly IMediator _mediator;

    public AppLookupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get lookup data with advanced filtering, sorting, and pagination
    /// </summary>
    /// <param name="request">Query request with filters, sorting, and pagination</param>
    /// <returns>Paginated lookup data</returns>
    [HttpPost("query")]
    public async Task<ActionResult<ApiResponse<PagedResult<AppLookupMasterDto>>>> GetLookupData([FromBody] QueryRequest request)
    {
        try
        {
            var query = new GetLookupDataQuery(request);
            var result = await _mediator.Send(query);
            return SuccessResponse(result, "Lookup data retrieved successfully");
        }
        catch (Exception ex)
        {
            return ErrorResponse<PagedResult<AppLookupMasterDto>>($"Error retrieving lookup data: {ex.Message}", 500);
        }
    }

    /// <summary>
    /// Get lookup master with details by code
    /// </summary>
    /// <param name="lookupCode">Lookup code (e.g., GENDER, MARITAL_STATUS)</param>
    /// <param name="includeDetails">Include lookup details</param>
    /// <returns>Lookup master with details</returns>
    [HttpGet("{lookupCode}")]
    public async Task<ActionResult<ApiResponse<AppLookupMasterDto>>> GetLookupByCode(
        string lookupCode,
        [FromQuery] bool includeDetails = true)
    {
        var query = new GetLookupMasterByCodeQuery(lookupCode, includeDetails);
        var lookup = await _mediator.Send(query);

        if (lookup == null)
            return ErrorResponse<AppLookupMasterDto>("Lookup not found", 404);

        return SuccessResponse(lookup, "Lookup retrieved successfully");
    }

    /// <summary>
    /// Get lookup details by master ID
    /// </summary>
    /// <param name="masterID">Master lookup ID</param>
    /// <returns>List of lookup details</returns>
    [HttpGet("{masterID:guid}/details")]
    public async Task<ActionResult<ApiResponse<IEnumerable<AppLookupDetailDto>>>> GetLookupDetails(Guid masterID)
    {
        var query = new GetLookupDetailsByMasterIdQuery(masterID);
        var details = await _mediator.Send(query);
        return SuccessResponse(details, "Lookup details retrieved successfully");
    }

    /// <summary>
    /// Create a new lookup master
    /// </summary>
    /// <param name="createLookupMasterDto">Lookup master creation data</param>
    /// <returns>Created lookup master</returns>
    [HttpPost("masters")]
    [Authorize(Roles = "Admin")] // Only admins can create lookup masters
    public async Task<ActionResult<ApiResponse<AppLookupMasterDto>>> CreateLookupMaster([FromBody] CreateAppLookupMasterDto createLookupMasterDto)
    {
        try
        {
            var command = new CreateAppLookupMasterCommand(createLookupMasterDto);
            var lookupMaster = await _mediator.Send(command);
            return CreatedResponse(lookupMaster, nameof(GetLookupByCode), new { lookupCode = lookupMaster.LookupCode }, "Lookup master created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<AppLookupMasterDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Create a new lookup detail
    /// </summary>
    /// <param name="createLookupDetailDto">Lookup detail creation data</param>
    /// <returns>Created lookup detail</returns>
    [HttpPost("details")]
    [Authorize(Roles = "Admin")] // Only admins can create lookup details
    public async Task<ActionResult<ApiResponse<AppLookupDetailDto>>> CreateLookupDetail([FromBody] CreateAppLookupDetailDto createLookupDetailDto)
    {
        try
        {
            var command = new CreateAppLookupDetailCommand(createLookupDetailDto);
            var lookupDetail = await _mediator.Send(command);
            return CreatedResponse(lookupDetail, nameof(GetLookupDetails), new { masterID = lookupDetail.LookupMasterID }, "Lookup detail created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<AppLookupDetailDto>(ex.Message, 400);
        }
    }
}