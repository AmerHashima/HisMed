using HIS.Application.Commands.SystemUser;
using HIS.Application.DTOs.SystemUser;
using HIS.Application.Queries.SystemUser;
using HIS.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers;

[Route("api/[controller]")]
public class SystemUserController : BaseApiController
{
    private readonly IMediator _mediator;

    public SystemUserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all system users
    /// </summary>
    /// <param name="includeInactive">Include inactive users in the result</param>
    /// <param name="roleId">Filter by role ID</param>
    /// <returns>List of system users</returns>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<SystemUserDto>>>> GetSystemUsers(
        [FromQuery] bool includeInactive = false,
        [FromQuery] int? roleId = null)
    {
        var query = new GetSystemUserListQuery 
        { 
            IncludeInactive = includeInactive,
            RoleId = roleId
        };
        var result = await _mediator.Send(query);
        return SuccessResponse(result, "System users retrieved successfully");
    }

    /// <summary>
    /// Get system user by ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>System user details</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApiResponse<SystemUserDto>>> GetSystemUser(Guid id)
    {
        var query = new GetSystemUserByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            return ErrorResponse<SystemUserDto>($"System user with ID {id} not found.", 404);
        }

        return SuccessResponse(result, "System user retrieved successfully");
    }

    /// <summary>
    /// Create a new system user
    /// </summary>
    /// <param name="createUserDto">User creation data</param>
    /// <returns>Created system user</returns>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<SystemUserDto>>> CreateSystemUser([FromBody] CreateSystemUserDto createUserDto)
    {
        var command = new CreateSystemUserCommand { CreateSystemUserDto = createUserDto };
        var result = await _mediator.Send(command);
        return CreatedResponse(result, nameof(GetSystemUser), new { id = result.Oid }, "System user created successfully");
    }

    /// <summary>
    /// Update an existing system user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="updateUserDto">User update data</param>
    /// <returns>Updated system user</returns>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ApiResponse<SystemUserDto>>> UpdateSystemUser(Guid id, [FromBody] UpdateSystemUserDto updateUserDto)
    {
        if (id != updateUserDto.Oid)
        {
            return ErrorResponse<SystemUserDto>("ID in URL does not match ID in request body.", 400);
        }

        var command = new UpdateSystemUserCommand { UpdateSystemUserDto = updateUserDto };
        var result = await _mediator.Send(command);
        return SuccessResponse(result, "System user updated successfully");
    }

    /// <summary>
    /// Delete a system user (soft delete)
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>Success status</returns>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ApiResponse>> DeleteSystemUser(Guid id)
    {
        var command = new DeleteSystemUserCommand { Id = id };
        var result = await _mediator.Send(command);
        
        if (!result)
        {
            return ErrorResponse($"System user with ID {id} not found.", 404);
        }

        return NoContentResponse();
    }
}