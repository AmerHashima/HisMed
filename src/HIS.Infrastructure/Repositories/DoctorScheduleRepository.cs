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

        public  async Task<List<DoctorSchedule?>>AddDoctorScheduelList(IEnumerable<DoctorSchedule> doctorSchedules,CancellationToken cancellation)
        {
           
            await context.DoctorSchedules.AddRangeAsync(doctorSchedules,cancellation);
            await context.SaveChangesAsync(cancellation);
            return doctorSchedules.ToList(); //return only the insterted records 
            
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
