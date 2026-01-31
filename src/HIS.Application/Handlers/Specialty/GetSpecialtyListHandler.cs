using AutoMapper;
using HIS.Application.DTOs.Specialty;
using HIS.Application.Queries.Specialty;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Specialty;

public class GetSpecialtyListHandler : IRequestHandler<GetSpecialtyListQuery, IEnumerable<SpecialtyDto>>
{
    private readonly ISpecialtyRepository _repository;
    private readonly IMapper _mapper;

    public GetSpecialtyListHandler(ISpecialtyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SpecialtyDto>> Handle(GetSpecialtyListQuery request, CancellationToken cancellationToken)
    {
        var specialties = request.ActiveOnly
            ? await _repository.GetActiveSpecialtiesAsync(cancellationToken)
            : await _repository.GetAllAsync(cancellationToken);

        return _mapper.Map<IEnumerable<SpecialtyDto>>(specialties);
    }
}