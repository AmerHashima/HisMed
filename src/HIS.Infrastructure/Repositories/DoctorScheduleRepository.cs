using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HIS.Infrastructure.Repositories
{
    public class DoctorScheduleRepository: BaseRepository<DoctorSchedule>,IDoctorScheduleRepository
    {
        private readonly HISDbContext context;

        public DoctorScheduleRepository(HISDbContext context):base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<DoctorSchedule>> GetAllSchedulesAsync(CancellationToken cancellationToken = default)
        {
            return await context.DoctorSchedules.Include(x => x.DayOfweek).Where(x => !x.IsDeleted).ToListAsync(cancellationToken);
        }

        public async Task<DoctorSchedule?> GetScheduelByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            return await context.DoctorSchedules
               .Include(x => x.DayOfweek)
                .Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Oid == Id,cancellationToken);
                
        }
        
    }
    }
