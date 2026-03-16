using HIS.Application.DTOs.DoctorSchedule;
using MediatR;

namespace HIS.Application.Commands.DoctorSchedule
{
    public sealed  record CreateDoctorScheduleBulkCommand( CreateDoctorScheduleBulkDto DoctorSechduel):IRequest<List<DoctorScheduleDto>>
    {

    }
}
