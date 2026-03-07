using AutoMapper;
using HIS.Application.Commands.Doctor;
using HIS.Application.DTOs.Doctor;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Doctor;

public class UpdateDoctorHandler : IRequestHandler<UpdateDoctorCommand, DoctorDto>
{
    private readonly IDoctorRepository _repository;
    private readonly IMapper _mapper;

    public UpdateDoctorHandler(IDoctorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DoctorDto> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        var existingDoctor = await _repository.GetByIdAsync(request.Doctor.Oid, cancellationToken);
        if (existingDoctor == null)
        {
            throw new KeyNotFoundException($"Doctor with ID {request.Doctor.Oid} not found");
        }

        // Check if license number is unique (excluding current doctor)
        if (!string.IsNullOrEmpty(request.Doctor.LicenseNumber) &&
            await _repository.LicenseNumberExistsAsync(request.Doctor.LicenseNumber, request.Doctor.Oid, cancellationToken))
        {
            throw new InvalidOperationException($"License number '{request.Doctor.LicenseNumber}' is already in use");
        }

        // Update properties
        existingDoctor.UserId = request.Doctor.UserId;
        existingDoctor.FirstNameAr = request.Doctor.FirstNameAr;
        existingDoctor.MiddleNameAr = request.Doctor.MiddleNameAr;
        existingDoctor.LastNameAr = request.Doctor.LastNameAr;
        existingDoctor.FirstNameEn = request.Doctor.FirstNameEn;
        existingDoctor.MiddleNameEn = request.Doctor.MiddleNameEn;
        existingDoctor.LastNameEn = request.Doctor.LastNameEn;
        existingDoctor.GenderId = request.Doctor.GenderId;
        existingDoctor.LicenseNumber = request.Doctor.LicenseNumber;
        existingDoctor.LicenseTypeId = request.Doctor.LicenseTypeId;
        existingDoctor.LicenseIssueDate = request.Doctor.LicenseIssueDate;
        existingDoctor.LicenseExpiryDate = request.Doctor.LicenseExpiryDate;
        existingDoctor.SpecialtyId = request.Doctor.SpecialtyId;
        existingDoctor.SubSpecialtyId = request.Doctor.SubSpecialtyId;
        existingDoctor.DepartmentId = request.Doctor.DepartmentId;
        existingDoctor.Mobile = request.Doctor.Mobile;
        existingDoctor.Phone = request.Doctor.Phone;
        existingDoctor.Email = request.Doctor.Email;
        existingDoctor.YearsOfExperience = request.Doctor.YearsOfExperience;
        existingDoctor.ConsultationFee = request.Doctor.ConsultationFee;
        existingDoctor.BranchId = request.Doctor.BranchId;
        existingDoctor.NphiesProviderId = request.Doctor.NphiesProviderId;
        existingDoctor.NphiesLicenseNumber = request.Doctor.NphiesLicenseNumber;
        existingDoctor.IsNphiesEnabled = request.Doctor.IsNphiesEnabled;
        existingDoctor.IsActive = request.Doctor.IsActive;
        existingDoctor.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existingDoctor, cancellationToken);
        return _mapper.Map<DoctorDto>(existingDoctor);
    }
}