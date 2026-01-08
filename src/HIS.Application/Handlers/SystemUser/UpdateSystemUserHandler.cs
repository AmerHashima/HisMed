using AutoMapper;
using HIS.Application.Commands.SystemUser;
using HIS.Application.DTOs.SystemUser;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.SystemUser;

public class UpdateSystemUserHandler : IRequestHandler<UpdateSystemUserCommand, SystemUserDto>
{
    private readonly ISystemUserRepository _repository;
    private readonly IMapper _mapper;

    public UpdateSystemUserHandler(ISystemUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<SystemUserDto> Handle(UpdateSystemUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _repository.GetByIdAsync(request.UpdateSystemUserDto.Oid, cancellationToken);
        if (existingUser == null)
        {
            throw new KeyNotFoundException($"SystemUser with ID {request.UpdateSystemUserDto.Oid} not found.");
        }

        // Check if username is unique (excluding current user)
        if (!await _repository.IsUsernameUniqueAsync(request.UpdateSystemUserDto.Username, request.UpdateSystemUserDto.Oid, cancellationToken))
        {
            throw new InvalidOperationException($"Username '{request.UpdateSystemUserDto.Username}' is already taken.");
        }

        // Check if email is unique (excluding current user, if provided)
        if (!string.IsNullOrEmpty(request.UpdateSystemUserDto.Email))
        {
            if (!await _repository.IsEmailUniqueAsync(request.UpdateSystemUserDto.Email, request.UpdateSystemUserDto.Oid, cancellationToken))
            {
                throw new InvalidOperationException($"Email '{request.UpdateSystemUserDto.Email}' is already in use.");
            }
        }

        // Update properties
        existingUser.Username = request.UpdateSystemUserDto.Username;
        existingUser.Email = request.UpdateSystemUserDto.Email;
        existingUser.Mobile = request.UpdateSystemUserDto.Mobile;
        existingUser.FirstName = request.UpdateSystemUserDto.FirstName;
        existingUser.MiddleName = request.UpdateSystemUserDto.MiddleName;
        existingUser.LastName = request.UpdateSystemUserDto.LastName;
        existingUser.Gender = request.UpdateSystemUserDto.Gender;
        existingUser.BirthDate = request.UpdateSystemUserDto.BirthDate;
        existingUser.RoleID = request.UpdateSystemUserDto.RoleID;
        existingUser.IsActive = request.UpdateSystemUserDto.IsActive;
        existingUser.TwoFactorEnabled = request.UpdateSystemUserDto.TwoFactorEnabled;
        existingUser.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existingUser, cancellationToken);
        return _mapper.Map<SystemUserDto>(existingUser);
    }
}