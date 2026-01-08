using FluentAssertions;
using HIS.Api.Controllers;
using HIS.Api.Models;
using HIS.Application.Commands.Auth;
using HIS.Application.DTOs.Auth;
using HIS.UnitTests.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HIS.UnitTests.Controllers;

public class AuthControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new AuthController(_mediatorMock.Object);
        
        // Setup HttpContext to avoid NullReferenceException
        var httpContext = new DefaultHttpContext();
        httpContext.TraceIdentifier = "test-trace-id";
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsAuthResponse()
    {
        // Arrange
        var loginDto = TestDataFactory.CreateTestLoginDto();
        var authResponse = TestDataFactory.CreateTestAuthResponse();

        _mediatorMock.Setup(x => x.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(authResponse);

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var apiResponse = actionResult.Value.Should().BeOfType<ApiResponse<AuthResponseDto>>().Subject;
        
        apiResponse.Success.Should().BeTrue();
        apiResponse.Data!.Token.Should().NotBeNullOrEmpty();
        apiResponse.Message.Should().Be("Login successful");
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var loginDto = TestDataFactory.CreateTestLoginDto();

        _mediatorMock.Setup(x => x.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(new UnauthorizedAccessException("Invalid credentials"));

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
        actionResult.StatusCode.Should().Be(401);
        
        var apiResponse = actionResult.Value.Should().BeOfType<ApiResponse<AuthResponseDto>>().Subject;
        apiResponse.Success.Should().BeFalse();
        apiResponse.Message.Should().Be("Invalid credentials");
    }

    [Fact]
    public async Task Register_WithValidData_ReturnsAuthResponse()
    {
        // Arrange
        var registerDto = TestDataFactory.CreateTestRegisterDto();
        var authResponse = TestDataFactory.CreateTestAuthResponse();

        _mediatorMock.Setup(x => x.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(authResponse);

        // Act
        var result = await _controller.Register(registerDto);

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var apiResponse = actionResult.Value.Should().BeOfType<ApiResponse<AuthResponseDto>>().Subject;
        
        apiResponse.Success.Should().BeTrue();
        apiResponse.Data!.Token.Should().NotBeNullOrEmpty();
        apiResponse.Message.Should().Be("Registration successful");
    }

    [Fact]
    public async Task Register_WithDuplicateUsername_ReturnsBadRequest()
    {
        // Arrange
        var registerDto = TestDataFactory.CreateTestRegisterDto();

        _mediatorMock.Setup(x => x.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(new InvalidOperationException("Username already exists"));

        // Act
        var result = await _controller.Register(registerDto);

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
        actionResult.StatusCode.Should().Be(400);
        
        var apiResponse = actionResult.Value.Should().BeOfType<ApiResponse<AuthResponseDto>>().Subject;
        apiResponse.Success.Should().BeFalse();
        apiResponse.Message.Should().Be("Username already exists");
    }

    [Fact]
    public async Task Logout_WithValidToken_ReturnsSuccess()
    {
        // Arrange
        var token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";
        _controller.HttpContext.Request.Headers["Authorization"] = token;

        _mediatorMock.Setup(x => x.Send(It.IsAny<LogoutCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        // Act
        var result = await _controller.Logout();

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var apiResponse = actionResult.Value.Should().BeOfType<ApiResponse>().Subject;
        
        apiResponse.Success.Should().BeTrue();
        apiResponse.Message.Should().Be("Logout successful");
    }

    [Fact]
    public async Task Logout_WithoutToken_ReturnsBadRequest()
    {
        // Arrange - No authorization header set

        // Act
        var result = await _controller.Logout();

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
        actionResult.StatusCode.Should().Be(400);
        
        var apiResponse = actionResult.Value.Should().BeOfType<ApiResponse>().Subject;
        apiResponse.Success.Should().BeFalse();
        apiResponse.Message.Should().Be("Token not found");
    }

    [Fact]
    public async Task RefreshToken_WithValidTokens_ReturnsNewAuthResponse()
    {
        // Arrange
        var refreshTokenDto = TestDataFactory.CreateTestRefreshTokenDto();
        var authResponse = TestDataFactory.CreateTestAuthResponse();

        _mediatorMock.Setup(x => x.Send(It.IsAny<RefreshTokenCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(authResponse);

        // Act
        var result = await _controller.RefreshToken(refreshTokenDto);

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var apiResponse = actionResult.Value.Should().BeOfType<ApiResponse<AuthResponseDto>>().Subject;
        
        apiResponse.Success.Should().BeTrue();
        apiResponse.Data!.Token.Should().NotBeNullOrEmpty();
        apiResponse.Message.Should().Be("Token refreshed successfully");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("InvalidToken")]
    [InlineData("Bearer")]
    [InlineData("Basic token")]
    public async Task GetTokenFromRequest_WithInvalidAuthHeader_ReturnsNull(string? authHeader)
    {
        // Arrange
        if (authHeader != null)
        {
            _controller.HttpContext.Request.Headers["Authorization"] = authHeader;
        }

        _mediatorMock.Setup(x => x.Send(It.IsAny<LogoutCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        // Act
        var result = await _controller.Logout();

        // Assert - Should return BadRequest when token is not found
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
        actionResult.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Login_WithInvalidModelState_ReturnsValidationErrors()
    {
        // Arrange
        var loginDto = new LoginDto { Username = "", Password = "" }; // Invalid data
        _controller.ModelState.AddModelError("Username", "Username is required");
        _controller.ModelState.AddModelError("Password", "Password is required");

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        result.Should().NotBeNull();
        var actionResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
        actionResult.StatusCode.Should().Be(400);
        
        var apiResponse = actionResult.Value.Should().BeOfType<ApiResponse<AuthResponseDto>>().Subject;
        apiResponse.Success.Should().BeFalse();
        apiResponse.Message.Should().Be("Validation failed");
        apiResponse.Errors.Should().NotBeNullOrEmpty();
    }
}