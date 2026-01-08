using AutoMapper;
using HIS.Application.Commands.SystemUser;
using HIS.Application.DTOs.SystemUser;
using HIS.Domain.Interfaces;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace HIS.Application.Handlers.SystemUser;

public class CreateSystemUserHandler : IRequestHandler<CreateSystemUserCommand, SystemUserDto>
{
    private readonly ISystemUserRepository _repository;
    private readonly IMapper _mapper;

    public CreateSystemUserHandler(ISystemUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<SystemUserDto> Handle(CreateSystemUserCommand request, CancellationToken cancellationToken)
    {
        // Check if username is unique
        if (!await _repository.IsUsernameUniqueAsync(request.CreateSystemUserDto.Username, cancellationToken: cancellationToken))
        {
            throw new InvalidOperationException($"Username '{request.CreateSystemUserDto.Username}' is already taken.");
        }

        // Check if email is unique (if provided)
        if (!string.IsNullOrEmpty(request.CreateSystemUserDto.Email))
        {
            if (!await _repository.IsEmailUniqueAsync(request.CreateSystemUserDto.Email, cancellationToken: cancellationToken))
            {
                throw new InvalidOperationException($"Email '{request.CreateSystemUserDto.Email}' is already in use.");
            }
        }

        // Hash the password
        var (hashedPassword, salt) = HashPassword(request.CreateSystemUserDto.Password);

        var systemUser = new Domain.Entities.SystemUser
        {
            Username = request.CreateSystemUserDto.Username,
            PasswordHash = hashedPassword,
            PasswordSalt = salt,
            Email = request.CreateSystemUserDto.Email,
            Mobile = request.CreateSystemUserDto.Mobile,
            FirstName = request.CreateSystemUserDto.FirstName,
            MiddleName = request.CreateSystemUserDto.MiddleName,
            LastName = request.CreateSystemUserDto.LastName,
            Gender = request.CreateSystemUserDto.Gender,
            BirthDate = request.CreateSystemUserDto.BirthDate,
            RoleID = request.CreateSystemUserDto.RoleID,
            IsActive = request.CreateSystemUserDto.IsActive,
            TwoFactorEnabled = request.CreateSystemUserDto.TwoFactorEnabled,
            CreatedAt = DateTime.UtcNow
        };

        var createdUser = await _repository.AddAsync(systemUser, cancellationToken);
        return _mapper.Map<SystemUserDto>(createdUser);
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