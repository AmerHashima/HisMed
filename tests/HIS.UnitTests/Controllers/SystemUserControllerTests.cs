using FluentAssertions;
using HIS.Api.Controllers;
using HIS.Api.Models;
using HIS.Application.Commands.SystemUser;
using HIS.Application.DTOs.SystemUser;
using HIS.Application.Queries.SystemUser;
using HIS.UnitTests.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HIS.UnitTests.Controllers;

public class SystemUserControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly SystemUserController _controller;

    public SystemUserControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new SystemUserController(_mediatorMock.Object);
        
        // Setup HttpContext to avoid NullReferenceException
        var httpContext = new DefaultHttpContext();
        httpContext.TraceIdentifier = "test-trace-id";
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }

    [Fact]
    public async Task GetSystemUsers_ReturnsSuccessResponse_WithUserList()
    {
        // Arrange
        var users = new List<SystemUserDto>
        {
            TestDataFactory.CreateTestSystemUserDto("user1"),
            TestDataFactory.CreateTestSystemUserDto("user2")
        };

        _mediatorMock.Setup(x => x.Send(It.IsAny<GetSystemUserListQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(users);

        // Act
        var result = await _controller.GetSystemUsers();

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var apiResponse = actionResult.Value.Should().BeOfType<ApiResponse<IEnumerable<SystemUserDto>>>().Subject;
        
        apiResponse.Success.Should().BeTrue();
        apiResponse.Data.Should().HaveCount(2);
        apiResponse.Message.Should().Be("System users retrieved successfully");
    }

    [Fact]
    public async Task GetSystemUsers_WithParameters_PassesCorrectQuery()
    {
        // Arrange
        var users = new List<SystemUserDto>();
        GetSystemUserListQuery? capturedQuery = null;

        _mediatorMock.Setup(x => x.Send(It.IsAny<GetSystemUserListQuery>(), It.IsAny<CancellationToken>()))
                    .Callback<IRequest<IEnumerable<SystemUserDto>>, CancellationToken>((query, ct) => capturedQuery = query as GetSystemUserListQuery)
                    .ReturnsAsync(users);

        // Act
        await _controller.GetSystemUsers(includeInactive: true, roleId: 2);

        // Assert
        capturedQuery.Should().NotBeNull();
        capturedQuery!.IncludeInactive.Should().BeTrue();
        capturedQuery.RoleId.Should().Be(2);
    }

    [Fact]
    public async Task GetSystemUser_WithValidId_ReturnsUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = TestDataFactory.CreateTestSystemUserDto();
        user.Oid = userId;

        _mediatorMock.Setup(x => x.Send(It.IsAny<GetSystemUserByIdQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(user);

        // Act
        var result = await _controller.GetSystemUser(userId);

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var apiResponse = actionResult.Value.Should().BeOfType<ApiResponse<SystemUserDto>>().Subject;
        
        apiResponse.Success.Should().BeTrue();
        apiResponse.Data!.Oid.Should().Be(userId);
    }

    [Fact]
    public async Task GetSystemUser_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _mediatorMock.Setup(x => x.Send(It.IsAny<GetSystemUserByIdQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((SystemUserDto?)null);

        // Act
        var result = await _controller.GetSystemUser(userId);

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
        actionResult.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task CreateSystemUser_WithValidData_ReturnsCreatedUser()
    {
        // Arrange
        var createDto = TestDataFactory.CreateTestCreateSystemUserDto();
        var createdUser = TestDataFactory.CreateTestSystemUserDto();

        _mediatorMock.Setup(x => x.Send(It.IsAny<CreateSystemUserCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(createdUser);

        // Act
        var result = await _controller.CreateSystemUser(createDto);

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
        var apiResponse = actionResult.Value.Should().BeOfType<ApiResponse<SystemUserDto>>().Subject;
        
        apiResponse.Success.Should().BeTrue();
        apiResponse.Data.Should().NotBeNull();
        apiResponse.Message.Should().Be("System user created successfully");
    }

    [Fact]
    public async Task UpdateSystemUser_WithValidData_ReturnsUpdatedUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var updateDto = TestDataFactory.CreateTestUpdateSystemUserDto(userId);
        var updatedUser = TestDataFactory.CreateTestSystemUserDto();
        updatedUser.Oid = userId;

        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateSystemUserCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(updatedUser);

        // Act
        var result = await _controller.UpdateSystemUser(userId, updateDto);

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var apiResponse = actionResult.Value.Should().BeOfType<ApiResponse<SystemUserDto>>().Subject;
        
        apiResponse.Success.Should().BeTrue();
        apiResponse.Data!.Oid.Should().Be(userId);
    }

    [Fact]
    public async Task UpdateSystemUser_WithMismatchedIds_ReturnsBadRequest()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var differentId = Guid.NewGuid();
        var updateDto = TestDataFactory.CreateTestUpdateSystemUserDto(differentId);

        // Act
        var result = await _controller.UpdateSystemUser(userId, updateDto);

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
        actionResult.StatusCode.Should().Be(400);
        
        var apiResponse = actionResult.Value.Should().BeOfType<ApiResponse<SystemUserDto>>().Subject;
        apiResponse.Success.Should().BeFalse();
        apiResponse.Message.Should().Be("ID in URL does not match ID in request body.");
    }

    [Fact]
    public async Task DeleteSystemUser_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteSystemUserCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteSystemUser(userId);

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<NoContentResult>().Subject;
        actionResult.StatusCode.Should().Be(204);
    }

    [Fact]
    public async Task DeleteSystemUser_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteSystemUserCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteSystemUser(userId);

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
        actionResult.StatusCode.Should().Be(404);
    }

    [Theory]
    [InlineData(true, null)]
    [InlineData(false, 1)]
    [InlineData(true, 2)]
    public async Task GetSystemUsers_WithDifferentParameters_CallsMediatorWithCorrectQuery(bool includeInactive, int? roleId)
    {
        // Arrange
        var users = new List<SystemUserDto>();
        GetSystemUserListQuery? capturedQuery = null;

        _mediatorMock.Setup(x => x.Send(It.IsAny<GetSystemUserListQuery>(), It.IsAny<CancellationToken>()))
                    .Callback<IRequest<IEnumerable<SystemUserDto>>, CancellationToken>((query, ct) => capturedQuery = query as GetSystemUserListQuery)
                    .ReturnsAsync(users);

        // Act
        await _controller.GetSystemUsers(includeInactive, roleId);

        // Assert
        capturedQuery.Should().NotBeNull();
        capturedQuery!.IncludeInactive.Should().Be(includeInactive);
        capturedQuery.RoleId.Should().Be(roleId);
    }
}