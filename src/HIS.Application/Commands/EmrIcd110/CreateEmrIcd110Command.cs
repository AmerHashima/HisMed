using HIS.Application.DTOs.Emr_Icd110;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Commands.EmrIcd110
{
    public sealed record CreateEmrIcd110Command(CreateEmr_icd110Dto Emr):IRequest<EmrResponseDto>
    {
    }
}
