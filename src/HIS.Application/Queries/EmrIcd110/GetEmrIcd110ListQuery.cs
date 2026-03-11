using HIS.Application.DTOs.Emr_Icd110;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Queries.EmrIcd110
{
    public sealed record GetEmrIcd110ListQuery(int? Level, string? CodeId, int? AustCode,int? Sex) : IRequest<IEnumerable<EmrResponseDto>>
    {
    }
}
