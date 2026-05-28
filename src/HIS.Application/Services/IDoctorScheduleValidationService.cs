using HIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Services
{
    public interface  IDoctorScheduleValidationService
    {
        Task<bool> HasOverLap(Guid BranchId,Guid SpecialityId, Guid DoctorId, DateOnly StartDate, DateOnly EndDate, IEnumerable<DoctorScheduleDetail> Newdetails, Guid? ExculdingSchedule=null, CancellationToken cancellation=default);
        
    }
}
