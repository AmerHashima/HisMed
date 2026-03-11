using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Commands.EmrIcd110
{
    public sealed record DeleteEmrIcd110Command(Guid Id):IRequest<bool>
    {
    }
}
