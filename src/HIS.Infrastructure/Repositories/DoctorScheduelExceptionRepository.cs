using HIS.Domain.Common;
using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Infrastructure.Repositories
{
    public class DoctorScheduelExceptionRepository:BaseRepository<DoctorScheduleException>,IDoctorscheduelExceptionRepository
    {
        private readonly HISDbContext context;

        public DoctorScheduelExceptionRepository(HISDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<DoctorScheduleException?> GetScheduelEXceptionByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            return await context.DoctorScheduleExceptions.Include(x => x.Days).Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Oid == Id, cancellationToken);
        }
    }
}
