using HIS.Application.DTOs.Auth;
using HIS.Application.DTOs.SystemUser;
using HIS.Domain.Entities;

namespace HIS.UnitTests.Helpers;

public static class TestDataFactory
{
    public static SystemUser CreateTestSystemUser(string username = "testuser")
    {
        return new SystemUser
        {
            Oid = Guid.NewGuid(),
            Username = username,
            PasswordHash = "hashedpassword",
            PasswordSalt = "salt",
            Email = $"{username}@test.com",
            Mobile = "+1234567890",
            FirstName = "Test",
            MiddleName = "Middle",
            LastName = "User",
            FullName = "Test Middle User",
            Gender = 'M',
            BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-25)),
            RoleID = 1,
            IsActive = true,
            TwoFactorEnabled = false,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static SystemUserDto CreateTestSystemUserDto(string username = "testuser")
    {
        var user = CreateTestSystemUser(username);
        return new SystemUserDto
        {
            Oid = user.Oid,
            Username = user.Username,
            Email = user.Email,
            Mobile = user.Mobile,
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            FullName = user.FullName,
            Gender = user.Gender,
            BirthDate = user.BirthDate,
            RoleID = user.RoleID,
            IsActive = user.IsActive,
            TwoFactorEnabled = user.TwoFactorEnabled,
            CreatedAt = user.CreatedAt
        };
    }

    public static LoginDto CreateTestLoginDto(string username = "testuser", string password = "Test123!")
    {
        return new LoginDto
        {
            Username = username,
            Password = password,
            RememberMe = false
        };
    }

    public static RegisterDto CreateTestRegisterDto(string username = "testuser")
    {
        return new RegisterDto
        {
            Username = username,
            Password = "Test123!",
            ConfirmPassword = "Test123!",
            Email = $"{username}@test.com",
            Mobile = "+1234567890",
            FirstName = "Test",
            MiddleName = "Middle",
            LastName = "User",
            Gender = "M",
            BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-25)),
            RoleID = 1
        };
    }

    public static AuthResponseDto CreateTestAuthResponse()
    {
        return new AuthResponseDto
        {
            Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
            RefreshToken = "refresh_token_here",
            Expires = DateTime.UtcNow.AddMinutes(60),
            User = CreateTestSystemUserDto()
        };
    }

    public static CreateSystemUserDto CreateTestCreateSystemUserDto(string username = "testuser")
    {
        return new CreateSystemUserDto
        {
            Username = username,
            Password = "Test123!",
            Email = $"{username}@test.com",
            Mobile = "+1234567890",
            FirstName = "Test",
            MiddleName = "Middle",
            LastName = "User",
            Gender = 'M',
            BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-25)),
            RoleID = 1,
            IsActive = true,
            TwoFactorEnabled = false
        };
    }

    public static UpdateSystemUserDto CreateTestUpdateSystemUserDto(Guid id, string username = "testuser")
    {
        return new UpdateSystemUserDto
        {
            Oid = id,
            Username = username,
            Email = $"{username}@test.com",
            Mobile = "+1234567890",
            FirstName = "Test",
            MiddleName = "Middle",
            LastName = "User",
            Gender = 'M',
            BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-25)),
            RoleID = 1,
            IsActive = true,
            TwoFactorEnabled = false
        };
    }

    public static RefreshTokenDto CreateTestRefreshTokenDto()
    {
        return new RefreshTokenDto
        {
            Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
            RefreshToken = "refresh_token_here"
        };
    }
}