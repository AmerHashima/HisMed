using AutoMapper;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Application.DTOs.Emr_Icd110;
using HIS.Application.Queries.EmrIcd110;
using HIS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.EmrIcd110
{
    public sealed class GetEmrIcd110ByIdHandler : IRequestHandler<GetEmrIcd110ByIdQuery, EmrResponseDto>
    {
        private readonly IEmrRepository repository;
        private readonly IMapper mapper;

        public GetEmrIcd110ByIdHandler(IEmrRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<EmrResponseDto> Handle(GetEmrIcd110ByIdQuery request, CancellationToken cancellationToken)
        {
            var emr = await repository.GetByIdAsync(request.Oid, cancellationToken);

            return emr == null ? null : mapper.Map<EmrResponseDto>(emr);
        }
    }
}
