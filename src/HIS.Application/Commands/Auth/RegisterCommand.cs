using HIS.Application.DTOs.Auth;
using MediatR;

namespace HIS.Application.Commands.Auth;

public class RegisterCommand : IRequest<AuthResponseDto>
{
    public RegisterDto RegisterDto { get; set; } = null!;
}