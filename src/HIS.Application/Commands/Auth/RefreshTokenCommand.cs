using HIS.Application.DTOs.Auth;
using MediatR;

namespace HIS.Application.Commands.Auth;

public class RefreshTokenCommand : IRequest<AuthResponseDto>
{
    public RefreshTokenDto RefreshTokenDto { get; set; } = null!;
}