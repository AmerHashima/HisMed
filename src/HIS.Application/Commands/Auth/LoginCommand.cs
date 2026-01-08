using HIS.Application.DTOs.Auth;
using MediatR;

namespace HIS.Application.Commands.Auth;

public class LoginCommand : IRequest<AuthResponseDto>
{
    public LoginDto LoginDto { get; set; } = null!;
}