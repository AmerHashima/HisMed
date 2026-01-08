using AutoMapper;
using HIS.Application.Commands.Auth;
using HIS.Application.DTOs.Auth;
using HIS.Application.DTOs.SystemUser;
using HIS.Application.Interfaces;
using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using MediatR;
using System.Security.Cryptography;

namespace HIS.Application.Handlers.Auth;

public class RegisterHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly ISystemUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public RegisterHandler(ISystemUserRepository userRepository, IJwtService jwtService, IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var dto = request.RegisterDto;

            // Check if username is unique
            if (!await _userRepository.IsUsernameUniqueAsync(dto.Username, cancellationToken: cancellationToken))
            {
                throw new InvalidOperationException($"Username '{dto.Username}' is already taken.");
            }

            // Check if email is unique (if provided)
            if (!string.IsNullOrEmpty(dto.Email))
            {
                if (!await _userRepository.IsEmailUniqueAsync(dto.Email, cancellationToken: cancellationToken))
                {
                    throw new InvalidOperationException($"Email '{dto.Email}' is already in use.");
                }
            }

            // Hash password
            var (hashedPassword, salt) = HashPassword(dto.Password);

            // Create user
            var user = new Domain.Entities.SystemUser
            {
                Username = dto.Username,
                PasswordHash = hashedPassword,
                PasswordSalt = salt,
                Email = dto.Email,
                Mobile = dto.Mobile,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                FullName = dto.FullName,
                //Gender = dto.Gender,
                BirthDate = dto.BirthDate,
                RoleID = dto.RoleID,
                IsActive = true,
                TwoFactorEnabled = false,
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _userRepository.AddAsync(user, cancellationToken);

            // Generate tokens
            var token = _jwtService.GenerateToken(createdUser);
            var refreshToken = _jwtService.GenerateRefreshToken();

            var userDto = _mapper.Map<SystemUserDto>(createdUser);

            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                Expires = DateTime.UtcNow.AddMinutes(60),
                User = userDto
            };
        }
        catch (Exception ex)
        {
            // Log exception (logging mechanism not shown here)
            throw new ApplicationException("An error occurred during registration.", ex);
        }
    }

    private static (string hashedPassword, string salt) HashPassword(string password)
    {
        using var rng = RandomNumberGenerator.Create();
        var saltBytes = new byte[32];
        rng.GetBytes(saltBytes);
        var salt = Convert.ToBase64String(saltBytes);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256);
        var hashedPassword = Convert.ToBase64String(pbkdf2.GetBytes(32));

        return (hashedPassword, salt);
    }
}