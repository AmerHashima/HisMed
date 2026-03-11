using HIS.Application.DTOs.Emr_Icd110;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Queries.EmrIcd110
{
   public sealed record GetEmrIcd110ByIdQuery(Guid Oid):IRequest<EmrResponseDto>
    {
    }
}
