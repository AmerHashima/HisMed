using AutoMapper;
using HIS.Application.DTOs.Doctor;
using HIS.Application.Queries.Doctor;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Doctor;

public class GetDoctorByIdHandler : IRequestHandler<GetDoctorByIdQuery, DoctorDto?>
{
    private readonly IDoctorRepository _repository;
    private readonly IMapper _mapper;

    public GetDoctorByIdHandler(IDoctorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DoctorDto?> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return doctor == null ? null : _mapper.Map<DoctorDto>(doctor);
    }
}