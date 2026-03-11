using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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

        public async  Task<List<DoctorSchedule?>> GetSchdeuleByDoctorIdAsync(Guid DoctorId, CancellationToken cancellation = default)
        {
            return await context.DoctorSchedules.Where(x => x.DoctorId == DoctorId).ToListAsync(cancellation) ;
        }

        public async Task<List<DoctorSchedule>> GetSchdeulesByStartTime(TimeOnly? StartTime, CancellationToken cancellation = default)
        {
            return await context.DoctorSchedules.Where(x => x.StartTime == StartTime).ToListAsync(); 
        }

        public async Task<DoctorSchedule?> GetScheduelByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            return await context.DoctorSchedules
               .Include(x => x.DayOfweek)
                .Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Oid == Id,cancellationToken);
                
        }
        
    }
    }
