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
        Task<bool> HasOverLap(Guid BranchId,Guid SpecialityId, Guid DoctorId, IEnumerable<DoctorScheduleDetail> details, Guid? ExculdingSchedule=null, CancellationToken cancellation=default);
        
    }
}
