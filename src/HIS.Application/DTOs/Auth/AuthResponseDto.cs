using HIS.Application.DTOs.SystemUser;

namespace HIS.Application.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    public SystemUserDto User { get; set; } = null!;
}