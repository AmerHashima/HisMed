using AutoMapper;
using HIS.Application.DTOs.Doctor;
using HIS.Application.Queries.Doctor;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Doctor;

public class GetDoctorListHandler : IRequestHandler<GetDoctorListQuery, IEnumerable<DoctorDto>>
{
    private readonly IDoctorRepository _repository;
    private readonly IMapper _mapper;

    public GetDoctorListHandler(IDoctorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DoctorDto>> Handle(GetDoctorListQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.Doctor> doctors;

        if (request.SpecialtyId.HasValue)
        {
            doctors = await _repository.GetDoctorsBySpecialtyAsync(request.SpecialtyId.Value, cancellationToken);
        }
        else if (request.BranchId.HasValue)
        {
            doctors = await _repository.GetDoctorsByBranchAsync(request.BranchId.Value, cancellationToken);
        }
        else if (request.ActiveOnly)
        {
            doctors = await _repository.GetActiveDoctorsAsync(cancellationToken);
        }
        else
        {
            doctors = await _repository.GetAllAsync(cancellationToken);
        }

        return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
    }
}