using HIS.Application.DTOs.DoctorSchedule;
using MediatR;

namespace HIS.Application.Commands.DoctorSchedule
{
    public sealed  record CreateDoctorScheduleBulkCommand( List<CreateDoctorScheduleBulkDto> DoctorSechduelList):IRequest<List<DoctorScheduleBulkResponseDto>>
    {

    }
}
