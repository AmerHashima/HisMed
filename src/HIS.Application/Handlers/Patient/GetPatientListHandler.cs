using AutoMapper;
using HIS.Application.DTOs.Patient;
using HIS.Application.Queries.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class GetPatientListHandler : IRequestHandler<GetPatientListQuery, IEnumerable<PatientDto>>
{
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;

    public GetPatientListHandler(IPatientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PatientDto>> Handle(GetPatientListQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.Patient> patients;

        // Priority-based filtering
        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            patients = await _repository.SearchPatientsAsync(request.SearchTerm, cancellationToken);
        }
        else if (request.BranchId.HasValue)
        {
            patients = await _repository.GetPatientsByBranchAsync(request.BranchId.Value, cancellationToken);
        }
        else if (request.GenderLookupId.HasValue)
        {
            patients = await _repository.GetPatientsByGenderAsync(request.GenderLookupId.Value, cancellationToken);
        }
        else if (request.BloodGroupLookupId.HasValue)
        {
            patients = await _repository.GetPatientsByBloodGroupAsync(request.BloodGroupLookupId.Value, cancellationToken);
        }
        else if (request.NationalityLookupId.HasValue)
        {
            patients = await _repository.GetPatientsByNationalityAsync(request.NationalityLookupId.Value, cancellationToken);
        }
        else if (!request.IncludeInactive)
        {
            patients = await _repository.GetActivePatientsAsync(cancellationToken);
        }
        else
        {
            patients = await _repository.GetAllAsync(cancellationToken);
        }

        // Apply pagination
        var pagedPatients = patients
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize);

        return _mapper.Map<IEnumerable<PatientDto>>(pagedPatients);
    }
}