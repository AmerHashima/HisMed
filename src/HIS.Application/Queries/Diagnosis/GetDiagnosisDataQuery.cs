using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.Diagnosis;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Queries.Diagnosis
{
    public sealed record GetDiagnosisDataQuery(QueryRequest QueryRequest):IRequest<PagedResult<DiagnosisDto>>
    {
    }
}
