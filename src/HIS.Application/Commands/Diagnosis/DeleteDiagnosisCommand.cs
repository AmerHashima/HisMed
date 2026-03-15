using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Commands.Diagnosis
{
    public sealed  record DeleteDiagnosisCommand(Guid Oid):IRequest<bool>
    {
    }
}
