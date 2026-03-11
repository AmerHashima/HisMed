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
    public class EmrRepository : BaseRepository<emr_icd110>, IEmrRepository
    {
        private readonly HISDbContext context;

        public EmrRepository(HISDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<emr_icd110>> GetEmrByAustCodeAsync(int AustCode, CancellationToken cancellationToken = default)
        {
            return await context.emr_icd110.Where(x => x.AustCode == AustCode).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<emr_icd110>> GetEmrByCodeIdAsync(string? CodeId, CancellationToken cancellationToken = default)
        {
            return await context.emr_icd110.Where(x => x.CodeId == CodeId).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<emr_icd110>> GetEmrByLevelAsync(int Level, CancellationToken cancellationToken = default)
        {
            return await context.emr_icd110.Where(x => x.Level == Level).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<emr_icd110>> GetEmrBySexAsync(int Sex, CancellationToken cancellationToken = default)
        {
            return await context.emr_icd110.Where(x => x.Sex == Sex).ToListAsync(cancellationToken);
        }
    }
}
