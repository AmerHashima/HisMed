using HIS.Domain.Entities;
using System.Security.Claims;

namespace HIS.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(SystemUser user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    Task<bool> IsTokenBlacklistedAsync(string token);
    Task BlacklistTokenAsync(string token, DateTime expiration);
}