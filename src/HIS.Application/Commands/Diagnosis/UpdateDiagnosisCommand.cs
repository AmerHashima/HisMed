using HIS.Application.DTOs.Diagnosis;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Commands.Diagnosis
{
    public sealed record UpdateDiagnosisCommand(UpdatedDiagnsisDto Diagonsis):IRequest<DiagnosisDto>
    {
    }
}
