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

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            patients = await _repository.SearchPatientsAsync(request.SearchTerm, cancellationToken);
        }
        else if (request.Gender.HasValue)
        {
            patients = await _repository.GetPatientsByGenderAsync(request.Gender.Value, cancellationToken);
        }
        else if (!string.IsNullOrEmpty(request.BloodGroup))
        {
            patients = await _repository.GetPatientsByBloodGroupAsync(request.BloodGroup, cancellationToken);
        }
        else if (!string.IsNullOrEmpty(request.Nationality))
        {
            patients = await _repository.GetPatientsByNationalityAsync(request.Nationality, cancellationToken);
        }
        else if (!request.IncludeInactive)
        {
            patients = await _repository.GetActivePatientsAsync(cancellationToken);
        }
        else
        {
            patients = await _repository.GetAllAsync(cancellationToken);
        }

        return _mapper.Map<IEnumerable<PatientDto>>(patients);
    }
}